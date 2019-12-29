using NUnit.Framework;
using FluentAssertions;

namespace MoonTools.FastCollections.Test
{
    public class BitSet512Test
    {
        [Test]
        public void Zero()
        {
            var bitSet = BitSet512.Zero;
            bitSet.AllFalse().Should().BeTrue();
            bitSet.AllTrue().Should().BeFalse();
        }

        [Test]
        public void Ones()
        {
            var bitSet = BitSet512.Ones;
            bitSet.AllTrue().Should().BeTrue();
            bitSet.AllFalse().Should().BeFalse();
        }

        [Test]
        public void Set()
        {
            var bitSet = BitSet512.Zero.Set(5);
            bitSet.Get(5).Should().BeTrue();
            bitSet.AllFalse().Should().BeFalse();

            bitSet = BitSet512.Zero.Set(132);
            bitSet.Get(132).Should().BeTrue();
            bitSet.AllFalse().Should().BeFalse();

            bitSet = BitSet512.Zero.Set(268);
            bitSet.Get(268).Should().BeTrue();
            bitSet.AllFalse().Should().BeFalse();

            bitSet = BitSet512.Zero.Set(450);
            bitSet.Get(450).Should().BeTrue();
            bitSet.AllFalse().Should().BeFalse();
        }

        [Test]
        public void UnSet()
        {
            var bitSet = BitSet512.Ones.UnSet(285).UnSet(24).UnSet(69);
            bitSet.Get(285).Should().BeFalse();
            bitSet.Get(24).Should().BeFalse();
            bitSet.Get(69).Should().BeFalse();
        }

        [Test]
        public void Get()
        {
            var bitSet = BitSet512.Zero.Set(359).Set(23).Set(63);
            bitSet.Get(359).Should().BeTrue();
            bitSet.Get(23).Should().BeTrue();
            bitSet.Get(63).Should().BeTrue();
        }

        [Test]
        public void Not()
        {
            var bitSet = ~BitSet512.Ones;
            bitSet.AllFalse().Should().BeTrue();
        }

        [Test]
        public void Or()
        {
            var a = BitSet512.Zero.Set(10);
            var b = BitSet512.Zero.Set(35);

            var or = a | b;
            or.Get(10).Should().BeTrue();
            or.Get(35).Should().BeTrue();
        }

        [Test]
        public void And()
        {
            var a = BitSet512.Zero.Set(10).Set(15).Set(20);
            var b = BitSet512.Zero.Set(10).Set(15).Set(18);

            var and = a & b;
            and.Get(10).Should().BeTrue();
            and.Get(15).Should().BeTrue();
            and.Get(18).Should().BeFalse();
        }

        [Test]
        public void Equal()
        {
            var zeroes = BitSet512.Zero;
            (zeroes == BitSet512.Zero).Should().BeTrue();

            var ones = BitSet512.Ones;
            (ones == BitSet512.Ones).Should().BeTrue();

            (zeroes != ones).Should().BeTrue();

            var a = BitSet512.Zero.Set(6);
            var b = BitSet512.Zero.Set(6);
            var c = BitSet512.Zero.Set(12);

            (a == b).Should().BeTrue();
            (a == c).Should().BeFalse();
        }
    }
}