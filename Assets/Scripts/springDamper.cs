// --------------------------------------------------------------------------------------------------------------------
// <copyright file="springDamper.cs" company="Guy Goudeau">
//   Property of Guy Goudeau, do not steal.
// </copyright>
// <summary>
//   Defines the SpringDamper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using UnityEngine;

    /// <summary>
    /// The spring damper class.
    /// </summary>
    public class SpringDamper
    {
        /// <summary>
        /// The spring constant, or the force that brings it back.
        /// </summary>
        private readonly float springConstant;

        /// <summary>
        /// The damping factor, or the force that limits how hard it comes back.
        /// </summary>
        private readonly float dampingFactor;

        /// <summary>
        /// The rest length, or the default starting length while at rest,
        /// </summary>
        private readonly float restLength;

        /// <summary>
        /// The first point between the spring dampers.
        /// </summary>
        private readonly Particle p1;

        /// <summary>
        /// The second point between the spring dampers.
        /// </summary>
        private readonly Particle p2;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpringDamper"/> class.
        /// </summary>
        /// <param name="p1">
        /// The first point between the spring dampers.
        /// </param>
        /// <param name="p2">
        /// The second point between the spring dampers.
        /// </param>
        /// <param name="sC">
        /// The spring constant, or the force that brings it back.
        /// </param>
        /// <param name="dF">
        /// The damping factor, or the force that limits how hard it comes back.
        /// </param>
        /// <param name="rL">
        /// The rest length, or the default starting length while at rest,
        /// </param>
        public SpringDamper(Particle p1, Particle p2, float sC, float dF, float rL)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.springConstant = sC;
            this.dampingFactor = dF;
            this.restLength = rL;
        }

        /// <summary>
        /// Computes the spring damper force.
        /// </summary>
        public void ComputeForce()
        {
            var dist = this.p2.Position - this.p1.Position; // e*
            var normDist = dist / dist.magnitude; // e, where l = Dist.magnitude

            var v1 = Vector3.Dot(normDist, this.p1.Velocity);
            var v2 = Vector3.Dot(normDist, this.p2.Velocity);

            var springDamperForce = (-this.springConstant * (this.restLength - dist.magnitude)) - (this.dampingFactor * (v1 - v2));

            var force1 = springDamperForce * normDist;
            var force2 = -force1;

            this.p1.AddForce(force1);
            this.p2.AddForce(force2);
        }
    }
}