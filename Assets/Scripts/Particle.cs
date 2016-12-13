// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Particle.cs" company="Guy Goudeau">
//   Property of Guy Goudeau, do not steal.
// </copyright>
// <summary>
//   Defines the Particle type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The particle class.
    /// </summary>
    public class Particle : MonoBehaviour
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        /// <param name="m">
        /// The particle's mass.
        /// </param>
        /// <param name="f">
        /// The particle's force.
        /// </param>
        /// <param name="ac">
        /// The particle's acceleration.
        /// </param>
        /// <param name="v">
        /// The particle's velocity.
        /// </param>
        /// <param name="p">
        /// The particle's position.
        /// </param>
        /// <param name="an">
        /// Whether this particle is an anchor or not.
        /// </param>
        /// <param name="n">
        /// The particle's list of neighbors.
        /// </param>
        public Particle(float m, Vector3 f, Vector3 ac, Vector3 v, Vector3 p, bool an, List<Particle> n)
        {
            this.Mass = m;
            this.Force = f;
            this.Acceleration = ac;
            this.Velocity = v;
            this.Position = p;
            this.Anchor = an;
            this.Neighbors = n;
        }

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        public float Mass { get; set; }

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        public Vector3 Force { get; set; }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public Vector3 Acceleration { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether anchor.
        /// </summary>
        public bool Anchor { get; set; }

        /// <summary>
        /// Gets or sets the neighbors.
        /// </summary>
        public List<Particle> Neighbors { get; set; }

        /// <summary>
        /// Updates the particle's position.
        /// </summary>
        /// <returns>
        /// The <see cref="Vector3"/>.
        /// </returns>
        public Vector3 UpdateParticle()
        {
            this.Acceleration = (1f / this.Mass) * this.Force;
            this.Velocity += this.Acceleration * Time.deltaTime;
            this.Position += this.Velocity * Time.deltaTime;

            return this.Position;
        }

        /// <summary>
        /// Adds two forces.
        /// </summary>
        /// <param name="force">
        /// The force to be added.
        /// </param>
        public void AddForce(Vector3 force)
        {
            this.Force += force;
        }
    }
}