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
        /// The rest length, or the default starting length while at rest.
        /// </param>
        public SpringDamper(Particle p1, Particle p2, float sC, float dF, float rL)
        {
            this.P1 = p1;
            this.P2 = p2;
            this.SpringConstant = sC;
            this.DampingFactor = dF;
            this.RestLength = rL;
        }

        /// <summary>
        /// Gets or sets the first particle.
        /// </summary>
        public Particle P1 { get; set; }

        /// <summary>
        /// Gets or sets the second particle.
        /// </summary>
        public Particle P2 { get; set; }

        /// <summary>
        /// Gets or sets the spring constant.
        /// </summary>
        public float SpringConstant { get; set; }

        /// <summary>
        /// Gets or sets the damping factor.
        /// </summary>
        public float DampingFactor { get; set; }

        /// <summary>
        /// Gets or sets the rest length.
        /// </summary>
        public float RestLength { get; set; }

        /// <summary>
        /// Computes the spring damper force.
        /// </summary>
        public void ComputeForce()
        {
            var dist = this.P2.Position - this.P1.Position; // e*
            var normDist = dist / dist.magnitude; // e, where l = Dist.magnitude

            var v1 = Vector3.Dot(normDist, this.P1.Velocity);
            var v2 = Vector3.Dot(normDist, this.P2.Velocity);

            var springDamperForce = (-this.SpringConstant * (this.RestLength - dist.magnitude)) - (this.DampingFactor * (v1 - v2));

            var force1 = springDamperForce * normDist;
            var force2 = -force1;

            this.P1.AddForce(force1);
            this.P2.AddForce(force2);
        }
    }
}