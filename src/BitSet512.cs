using System;
using System.Collections.Generic;

namespace MoonTools.FastCollections
{
    public static unsafe class MemoryHelper
    {
        public static void Copy(uint* src, uint* dest, int count)
        {
            for (; count != 0; count--) *dest++ = *src++;
        }

        public static void Fill(uint* p, int count, uint value)
        {
            for (; count != 0; count--) *p++ = value;
        }

        public static void And(uint* p, uint* q, uint* result, int count)
        {
            for (; count != 0; count--) *result++ = *p++ & *q++;
        }

        public static void Or(uint* p, uint* q, uint* result, int count)
        {
            for (; count != 0; count--) *result++ = *p++ | *q++;
        }

        public static void Not(uint* p, uint* result, int count)
        {
            for (; count != 0; count--) *result++ = ~*p++;
        }

        public static bool Equal(uint* p, uint* q, int count)
        {
            for (; count != 0; count--) if (*p++ != *q++) { return false;  }
            return true;
        }

        public static bool NotEqual(uint* p, uint* q, int count)
        {
            for (; count != 0; count--) if (*p++ == *q++) { return false; }
            return true;
        }
    }

    public unsafe struct BitSet512 : IEquatable<BitSet512>
    {
        public static BitSet512 Zero { get; } = new BitSet512(0);
        public static BitSet512 Ones { get; } = new BitSet512(uint.MaxValue);

        private const int _uintLength = 16;

        private fixed uint _buffer[_uintLength];

        public BitSet512(uint value)
        {
            fixed (uint* p = _buffer) MemoryHelper.Fill(p, _uintLength, value);
        }

        public BitSet512(uint* src)
        {
            fixed (uint* dest = _buffer) MemoryHelper.Copy(src, dest, _uintLength);
        }

        public static BitSet512 operator &(BitSet512 a, BitSet512 b)
        {
            var tmp = stackalloc uint[_uintLength];
            MemoryHelper.And(a._buffer, b._buffer, tmp, _uintLength);
            return new BitSet512(tmp);
        }

        public static BitSet512 operator |(BitSet512 a, BitSet512 b)
        {
            var tmp = stackalloc uint[_uintLength];
            MemoryHelper.Or(a._buffer, b._buffer, tmp, _uintLength);
            return new BitSet512(tmp);
        }

        public static BitSet512 operator ~(BitSet512 a)
        {
            var tmp = stackalloc uint[_uintLength];
            MemoryHelper.Not(a._buffer, tmp, _uintLength);
            return new BitSet512(tmp);
        }

        public static bool operator ==(BitSet512 left, BitSet512 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BitSet512 left, BitSet512 right)
        {
            return !(left == right);
        }

        public BitSet512 Set(int index)
        {
            var tmp = stackalloc uint[_uintLength];
            fixed (uint* p = _buffer) MemoryHelper.Copy(p, tmp, _uintLength);
            tmp[index / 32] |= (uint)(1 << index % 32);
            return new BitSet512(tmp);
        }

        public BitSet512 UnSet(int index)
        {
            var tmp = stackalloc uint[_uintLength];
            fixed (uint* p = _buffer) MemoryHelper.Copy(p, tmp, _uintLength);
            tmp[index / 32] &= ~(uint)(1 << index % 32);
            return new BitSet512(tmp);
        }

        public bool Get(int bitIndex)
        {
            var bitInt = (uint)(1 << bitIndex % 32);
            return (_buffer[bitIndex / 32] & bitInt) == bitInt;
        }

        public bool AllTrue()
        {
            return this == Ones;
        }

        public bool AllFalse()
        {
            return this == Zero;
        }

        public static BitSet512 BitwiseAnd(BitSet512 left, BitSet512 right)
        {
            return left & right;
        }

        public static BitSet512 BitwiseOr(BitSet512 left, BitSet512 right)
        {
            return left | right;
        }

        public static BitSet512 OnesComplement(BitSet512 bitSet)
        {
            return ~bitSet;
        }

        public override bool Equals(object obj)
        {
            return obj is BitSet512 set && Equals(set);
        }

        public bool Equals(BitSet512 other)
        {
            fixed (uint* p = _buffer) return MemoryHelper.Equal(p, other._buffer, _uintLength);
        }

        public override int GetHashCode()
        {
            var hc = 0;
            for (var i = 0; i < _uintLength; i++)
            {
                hc ^= _buffer[i].GetHashCode();
            }
            return hc;
        }
    }
}
