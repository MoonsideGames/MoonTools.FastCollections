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
            var bitSet = BitSet512.Ones.UnSet(285);
            bitSet.Get(285).Should().BeFalse();
            bitSet.Set(285).AllTrue().Should().BeTrue();
        }

        [Test]
        public void Get()
        {
            var bitSet = BitSet512.Zero.Set(359);
            bitSet.Get(359).Should().BeTrue();
            bitSet.UnSet(359).AllFalse().Should().BeTrue();
        }

        [Test]
        public void Not()
        {
            var bitSet = ~BitSet512.Ones;
            bitSet.AllFalse().Should().BeTrue();
        }
    }
}