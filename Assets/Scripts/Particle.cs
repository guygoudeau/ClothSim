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
/*
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
*/

        /// <summary>
        /// The mass.
        /// </summary>
        private float mass;

        /// <summary>
        /// The force.
        /// </summary>
        private Vector3 force;

        /// <summary>
        /// The acceleration.
        /// </summary>
        private Vector3 acceleration;

        /// <summary>
        /// The velocity.
        /// </summary>
        private Vector3 velocity;

        /// <summary>
        /// The position.
        /// </summary>
        private Vector3 position;

        /// <summary>
        /// Determines whether anchor.
        /// </summary>
        private bool anchor;

        /// <summary>
        /// The list of neighbors.
        /// </summary>
        private List<Particle> neighbors;

        /// <summary>
        /// Gets or sets the mass.
        /// </summary>
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public float Mass
        {
            get
            {
                return this.mass;
            }

            set
            {
                this.mass = value;
            }
        }

        /// <summary>
        /// Gets or sets the force.
        /// </summary>
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        public Vector3 Force
        {
            get
            {
                return this.force;
            }

            set
            {
                this.force = value;
            }
        }

        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public Vector3 Acceleration
        {
            get
            {
                return this.acceleration;
            }

            set
            {
                this.acceleration = value;
            }
        }

        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public Vector3 Velocity
        {
            get
            {
                return this.velocity;
            }

            set
            {
                this.velocity = value;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public Vector3 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether anchor.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public bool Anchor
        {
            get
            {
                return this.anchor;
            }

            set
            {
                this.anchor = value;
            }
        }

        /// <summary>
        /// Gets or sets the neighbors.
        /// </summary>
        // ReSharper disable once ConvertToAutoProperty
        public List<Particle> Neighbors
        {
            get
            {
                return this.neighbors;
            }

            set
            {
                this.neighbors = value;
            }
        }

        /// <summary>
        /// Updates the particle's position.
        /// </summary>
        /// <returns>
        /// The <see cref="Vector3"/>.
        /// </returns>
        public Vector3 UpdateParticle()
        {
            this.Acceleration = (1f / this.mass) * this.Force;
            this.Velocity += this.Acceleration * Time.deltaTime;
            this.Position += this.Velocity * Time.deltaTime;

            return this.Position;
        }

        /// <summary>
        /// Adds two forces.
        /// </summary>
        /// <param name="forceAdded">
        /// The force to be added.
        /// </param>
        public void AddForce(Vector3 forceAdded)
        {
            this.force += forceAdded;
        }

        /// <summary>
        /// The awake.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void Awake()
        {
            this.mass = 1;
        }
    }
}