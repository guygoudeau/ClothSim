// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Triangle.cs" company="Guy Goudeau">
//   Property of Guy Goudeau, do not steal.
// </copyright>
// <summary>
//   Defines the Triangle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using UnityEngine;

    /// <summary>
    /// The triangle class.
    /// </summary>
    public class Triangle 
    {
        /// <summary>
        /// The first particle point of the triangle.
        /// </summary>
        private readonly Particle p1;

        /// <summary>
        /// The second particle point of the triangle.
        /// </summary>
        private readonly Particle p2;

        /// <summary>
        /// The third particle point of the triangle.
        /// </summary>
        private readonly Particle p3;

        /// <summary>
        /// The density of the air.
        /// </summary>
        private float p;

        /// <summary>
        /// The coefficient of drag for the triangle.
        /// </summary>
        private float cd;

        /// <summary>
        /// The cross sectional area of the triangle.
        /// </summary>
        private float a;

        /// <summary>
        /// The area of the triangle.
        /// </summary>
        private float ao; 

        /// <summary>
        /// The normal of the surface.
        /// </summary>
        private Vector3 n;

        /// <summary>
        /// The relative velocity.
        /// </summary>
        private Vector3 v;

        /// <summary>
        /// The velocity of the triangle.
        /// </summary>
        private Vector3 surfaceVelocity;

        /// <summary>
        /// The velocity of the air.
        /// </summary>
        private Vector3 airVelocity; 

        /// <summary>
        /// The aerodynamic force.
        /// </summary>
        private Vector3 aeroForce; 

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="p1">
        /// The p 1.
        /// </param>
        /// <param name="p2">
        /// The p 2.
        /// </param>
        /// <param name="p3">
        /// The p 3.
        /// </param>
        public Triangle(Particle p1, Particle p2, Particle p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }

        /// <summary>
        /// The aero force.
        /// </summary>
        /// <param name="density">
        /// The density.
        /// </param>
        /// <param name="drag">
        /// The drag.
        /// </param>
        /// <param name="airVel">
        /// The air velocity.
        /// </param>
        public void AeroForce(float density, float drag, Vector3 airVel)
        {
            this.p = density;
            this.cd = drag;
            this.airVelocity = airVel;

            this.surfaceVelocity = (this.p1.Velocity + this.p2.Velocity + this.p3.Velocity) / 3;
            this.v = this.surfaceVelocity - this.airVelocity;
            this.n = Vector3.Cross(this.p2.transform.position - this.p1.transform.position, this.p3.transform.position - this.p1.transform.position) /
                Vector3.Cross(this.p2.transform.position - this.p1.transform.position, this.p3.transform.position - this.p1.transform.position).magnitude;
            this.ao = 0.5f * Vector3.Cross(this.p2.transform.position - this.p1.transform.position, this.p3.transform.position - this.p1.transform.position).magnitude;
            this.a = this.ao * (Vector3.Dot(this.v, this.n) / this.v.magnitude);

            this.aeroForce = -0.5f * this.p * (this.v.magnitude * this.v.magnitude) * this.cd * this.a * this.n;

            this.p1.AddForce(this.aeroForce / 3);
            this.p2.AddForce(this.aeroForce / 3);
            this.p3.AddForce(this.aeroForce / 3);
        }
    }
}
