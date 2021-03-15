using System;
using System.Collections.Generic;
using System.Text;

namespace ComparableEquatableBase
{
    /// <summary>
    /// comparable equatable base
    /// </summary>
    /// <typeparam name="TImpl"></typeparam>
    /// <typeparam name="TAdapted"></typeparam>
    public abstract class ComparableEquatableBase<TImpl, TAdapted> : IEquatable<TImpl>, IComparable<TImpl>, IComparable where TImpl : class
    {
        /// <summary>
        /// deriver impl to provide a TAdapted
        /// </summary>
        /// <returns></returns>
        protected abstract TAdapted GetComparableEquatableAdaptation();

        /// <summary>
        /// adapt for equality
        /// </summary>
        /// <param name="oother"></param>
        /// <returns></returns>
        private (IEquatable<TAdapted>, IEquatable<TAdapted>) AdaptE(object oother)
        {
            var other = oother as ComparableEquatableBase<TImpl, TAdapted>;
            var oadapted = (IEquatable<TAdapted>)other.GetComparableEquatableAdaptation();
            var meadapted = (IEquatable<TAdapted>)this.GetComparableEquatableAdaptation();
            return (oadapted, meadapted);
        }

        /// <summary>
        /// adapt for comparable
        /// </summary>
        /// <param name="oother"></param>
        /// <returns></returns>
        private (IComparable<TAdapted>, IComparable<TAdapted>) AdaptC(object oother)
        {
            var other = oother as ComparableEquatableBase<TImpl, TAdapted>;
            var oadapted = (IComparable<TAdapted>)other.GetComparableEquatableAdaptation();
            var meadapted = (IComparable<TAdapted>)this.GetComparableEquatableAdaptation();
            return (oadapted, meadapted);
        }

        /// <summary>
        /// do equals check
        /// </summary>
        /// <param name="oother"></param>
        /// <returns></returns>
        private bool DoEquals(object oother)
        {
            (var oadapted, var meadapted) = AdaptE(oother);
            return (oadapted?.Equals(meadapted)).GetValueOrDefault(false);
        }

        /// <summary>
        /// do compare
        /// </summary>
        /// <param name="oother"></param>
        /// <returns></returns>
        private int DoCompareTo(object oother)
        {
            if (oother == null)
            {
                throw new ArgumentNullException(nameof(oother), "comparison to null isn't a supported case (An equality check would be OK)");
            }

            (var oadapted, var meadapted) = AdaptC(oother);
            return meadapted.CompareTo((TAdapted)oadapted);
        }

        /// <summary>
        /// typed equals impl
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TImpl other)
        {
            return DoEquals(other);
        }

        /// <summary>
        /// obj equals impl
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            return DoEquals(other);
        }

        /// <summary>
        /// obj hashcode impl
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetComparableEquatableAdaptation().GetHashCode();
        }

        /// <summary>
        /// typed compare
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(TImpl other)
        {
            return DoCompareTo(other);
        }

        /// <summary>
        /// icomparable impl
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(object other)
        {
            return DoCompareTo(other);
        }
    }
}
