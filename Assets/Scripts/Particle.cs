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
        /// The mass.
        /// </summary>
        private readonly float mass;

        /// <summary>
        /// The acceleration.
        /// </summary>
        private Vector3 acceleration = Vector3.zero;

        /// <summary>
        /// The force.
        /// </summary>
        private Vector3 force = Vector3.zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        /// <param name="mass">
        /// The mass.
        /// </param>
        /// <param name="anchor">
        /// The anchor.
        /// </param>
        /// <param name="neighbors">
        /// The neighbors.
        /// </param>
        public Particle(float mass, bool anchor, List<Particle> neighbors)
        {
            this.mass = mass;
            this.Anchor = anchor;
            this.Neighbors = neighbors;
        }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector3 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the velocity.
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
        /// The update particle.
        /// </summary>
        /// <returns>
        /// The <see cref="Vector3"/>.
        /// </returns>
        public Vector3 UpdateParticle()
        {
            this.acceleration = (1f / this.mass) * this.force;
            this.Velocity += this.acceleration * Time.deltaTime;
            this.Position += this.Velocity * Time.deltaTime;

            return this.Position;
        }

        /// <summary>
        /// The add force.
        /// </summary>
        /// <param name="force2">
        /// The force.
        /// </param>
        public void AddForce(Vector3 force2)
        {
            this.force += force2;
        }
    }
}