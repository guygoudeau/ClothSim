// --------------------------------------------------------------------------------------------------------------------
// <copyright file="clothSim.cs" company="Guy Goudeau">
//   Property of Guy Goudeau, do not steal.
// </copyright>
// <summary>
//   Defines the clothSim type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The cloth simulation class.
    /// </summary>
    public class ClothSim : MonoBehaviour
    { 
        /// <summary>
        /// The width of the particle grid.
        /// </summary>
        [SerializeField]
        private const int Width = 7;

        /// <summary>
        /// The height of the particle grid.
        /// </summary>
        [SerializeField]
        private const int Height = 7;

        /// <summary>
        /// The air density.
        /// </summary>
        private const float AirDensity = 1;

        /// <summary>
        /// The drag coefficient.
        /// </summary>
        private const float DragCoefficient = 1;

        /// <summary>
        /// The particle list.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Particle> particleList;

        /// <summary>
        /// The spring damper list.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<SpringDamper> springDamperList;

        /// <summary>
        /// The temp spring damper list.
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Local
        private List<SpringDamper> tempSpringDamperList;

        /// <summary>
        /// The triangle list.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<Triangle> triangleList;

        /// <summary>
        /// The temp triangle list.
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Local
        private List<Triangle> tempTriangleList;

        /// <summary>
        /// The line list.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private List<GameObject> lineList;

        /// <summary>
        /// The temp line list.
        /// </summary>
        private List<GameObject> tempLineList;

        /// <summary>
        /// The spring constant.
        /// </summary>
        [Range(0, 100)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float springConstant;

        /// <summary>
        /// The damping factor.
        /// </summary>
        [Range(0, 10)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float dampingFactor;

        /// <summary>
        /// The rest length.
        /// </summary>
        [Range(0, 5)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float restLength;

        /// <summary>
        /// The gravity.
        /// </summary>
        [Range(-10, 10)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float gravity;

        /// <summary>
        /// The air velocity.
        /// </summary>
        [Range(-5, 5)]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private float airVelocity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClothSim"/> class.
        /// </summary>
        /// <param name="sC">
        /// The spring constant.
        /// </param>
        /// <param name="dF">
        /// The damping factor.
        /// </param>
        /// <param name="rL">
        /// The rest length.
        /// </param>
        /// <param name="g">
        /// The gravity.
        /// </param>
        /// <param name="aV">
        /// The air velocity.
        /// </param>
        /// <param name="pP">
        /// The particle prefab.
        /// </param>
        /// <param name="lP">
        /// The line prefab.
        /// </param>
        /// <param name="lM">
        /// The line material.
        /// </param>
        public ClothSim(float sC, float dF, float rL, float g, float aV, GameObject pP, GameObject lP, Material lM)
        {
            this.springConstant = sC;
            this.dampingFactor = dF;
            this.restLength = rL;
            this.gravity = g;
            this.airVelocity = aV;
            this.ParticlePrefab = pP;
            this.LinePrefab = lP;
            this.LineMaterial = lM;
            this.particleList = new List<Particle>();
            this.springDamperList = new List<SpringDamper>();
            this.tempSpringDamperList = new List<SpringDamper>();
            this.triangleList = new List<Triangle>();
            this.tempTriangleList = new List<Triangle>();
            this.lineList = new List<GameObject>();
            this.tempLineList = new List<GameObject>();
        }

        /// <summary>
        /// Gets or sets the spring constant.
        /// </summary>
        public float SpringConstant
        {
            get
            {
                return this.springConstant;
            }

            set
            {
                this.springConstant = value;
            }
        }

        /// <summary>
        /// Gets or sets the damping factor.
        /// </summary>
        public float DampingFactor
        {
            get
            {
                return this.dampingFactor;
            }

            set
            {
                this.dampingFactor = value;
            }
        }

        /// <summary>
        /// Gets or sets the rest length.
        /// </summary>
        public float RestLength
        {
            get
            {
                return this.restLength;
            }

            set
            {
                this.restLength = value;
            }
        }

        /// <summary>
        /// Gets or sets the gravity.
        /// </summary>
        public float Gravity
        {
            get
            {
                return this.gravity;
            }

            set
            {
                this.gravity = value;
            }
        }

        /// <summary>
        /// Gets or sets the air velocity.
        /// </summary>
        public float AirVelocity
        {
            get
            {
                return this.airVelocity;
            }

            set
            {
                this.airVelocity = value;
            }
        }

        /// <summary>
        /// Gets or sets the particle prefab.
        /// </summary>
        public GameObject ParticlePrefab { get; set; }

        /// <summary>
        /// Gets or sets the line prefab.
        /// </summary>
        public GameObject LinePrefab { get; set; }

        /// <summary>
        /// Gets or sets the line material.
        /// </summary>
        public Material LineMaterial { get; set; }

        /// <summary>
        /// Handles actions that need to happen at program start.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void Start()
        {
            // ReSharper disable once UnusedVariable
            var clothSim = new ClothSim(this.SpringConstant, this.DampingFactor, this.RestLength, this.Gravity, this.AirVelocity, this.ParticlePrefab, this.LinePrefab, this.LineMaterial);

            this.SpawnParticles();
            this.SetNeighborsAndDampers();
            this.SetTriangles();

            foreach (var p in this.particleList)
            {
                // set starting position values for every particle
                p.Position = p.transform.position;
            }
        }

        /// <summary>
        /// Updates actions every frame.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void Update()
        {
            this.UpdateForces();
        }

        /// <summary>
        /// Creates a grid of particles.
        /// </summary>
        private void SpawnParticles()
        {
            var xPos = 0f; // for determining position of particles on x axis 
            var yPos = 0f; // for determining position of particles on y axis 

            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    var particle = Instantiate(this.ParticlePrefab, new Vector3(xPos, yPos, 0), new Quaternion()) as GameObject; // spawn particle prefab
                    if (particle != null)
                    {
                        // add that particle to the particle list
                        this.particleList.Add(particle.GetComponent<Particle>());  
                    }

                    xPos += 2; // x margin of 2 between particles
                }

                yPos += 2; // y margin of 2 between particles
                xPos = 0; // reset x for next loop
            }

            this.particleList[this.particleList.Count - 1].Anchor = true; // set top right anchor
            this.particleList[this.particleList.Count - Width].Anchor = true; // set top left anchor
        }

        /// <summary>
        /// Resets temp lists, computes and applies forces, and draw lines.
        /// </summary>
        private void UpdateForces() 
        {
            this.tempSpringDamperList = new List<SpringDamper>(); // reset the temporary line list
            foreach (var sd in this.springDamperList)
            {
                // add every spring damper in the spring damper list to temporary spring damper list
                this.tempSpringDamperList.Add(sd);
            }

            this.tempTriangleList = new List<Triangle>(); // reset the temporary line list
            foreach (var t in this.triangleList) 
            {
                // add every triangle in the triangle list to temporary triangle list
                this.tempTriangleList.Add(t);
            }

            this.tempLineList = new List<GameObject>(); // reset the temporary line list
            foreach (var l in this.lineList) 
            {
                // add every line in the line list to temporary line list
                this.tempLineList.Add(l);
            }

            foreach (var p in this.particleList) 
            {
                // apply gravity to every particle
                p.Force = (this.gravity * Vector3.down) * p.Mass;
            }

            foreach (var sd in this.springDamperList) 
            {
                // compute and apply forces to every spring damper
                sd.DampingFactor = this.dampingFactor;
                sd.SpringConstant = this.springConstant;
                sd.RestLength = this.restLength;
                sd.ComputeForce();
            }

            foreach (var t in this.triangleList) 
            {
                // compute and apply aerodynamic force to every triangle
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (this.airVelocity != 0)
                {
                    t.AeroForce(AirDensity, DragCoefficient, this.airVelocity * Vector3.forward);
                }
            }

            foreach (var p in this.particleList) 
            {
                // update the position of every particle that isn't an anchor
                if (p.Anchor == false)
                {
                    p.transform.position = p.UpdateParticle();
                }
                else
                {
                    p.Position = p.transform.position;
                }
            }

            for (var i = 0; i < this.tempLineList.Count; i++) 
            {
                // set line positions, material and width
                var line = this.tempLineList[i].GetComponent<LineRenderer>();
                line.SetPosition(0, this.springDamperList[i].P1.Position);
                line.SetPosition(1, this.springDamperList[i].P2.Position);
                line.material = this.LineMaterial;
                line.SetWidth(.2f, .2f);
            }
        }

        /// <summary>
        /// Finds particle neighbors and creates spring dampers and line renderers there.
        /// </summary>
        private void SetNeighborsAndDampers()  
        {
            foreach (var p in this.particleList)
            {
                p.Neighbors = new List<Particle>();
                var particleNum = 0;

                for (var i = 0; i < this.particleList.Count; i++)
                {
                    // go through however many particles are in the list
                    if (this.particleList[i] == p) 
                    {
                        // if the index of a particle in the list equals the current particle, set that particle's number to be the same as that index
                        particleNum = i;
                    }
                }

                // Note: how many heights you have is how many times you iterate through widths. Create spring dampers for every neighbor
                if (particleNum + Width < this.particleList.Count) 
                {
                    // above neighbor
                    p.Neighbors.Add(this.particleList[particleNum + Width]); // add that particle to the neighbor list
                    var sd = new SpringDamper(p, this.particleList[particleNum + Width], this.springConstant, this.dampingFactor, this.restLength); // create a spring damper
                    this.springDamperList.Add(sd); // add that spring damper to the spring damper list
                    this.lineList.Add(this.CreateLineRenderers(sd)); // create a line renderer here and add it to the line list
                }

                if ((particleNum + 1) % Width > particleNum % Width) 
                {
                    // immediate right neighbor
                    p.Neighbors.Add(this.particleList[particleNum + 1]); // add that particle to the neighbor list
                    var sd = new SpringDamper(p, this.particleList[particleNum + 1], this.springConstant, this.dampingFactor, this.restLength); // create a spring damper
                    this.springDamperList.Add(sd); // add that spring damper to the spring damper list
                    this.lineList.Add(this.CreateLineRenderers(sd)); // create a line renderer here and add it to the line list
                }

                if (particleNum + Width + 1 < this.particleList.Count && (particleNum + 1) % Width > particleNum % Width)     
                {
                    // top right neighbor
                    p.Neighbors.Add(this.particleList[particleNum + Width + 1]); // add that particle to the neighbor list
                    var sd = new SpringDamper(p, this.particleList[particleNum + Width + 1], this.springConstant, this.dampingFactor, this.restLength); // create a spring damper
                    this.springDamperList.Add(sd); // add that spring damper to the spring damper list
                    this.lineList.Add(this.CreateLineRenderers(sd)); // create a line renderer here and add it to the line list
                }

                if (particleNum + Width - 1 < this.particleList.Count && (particleNum + Width - 1) % Width < particleNum % Width) 
                {
                    // top left neighbor
                    p.Neighbors.Add(this.particleList[particleNum + Width - 1]); // add that particle to the neighbor list
                    var sd = new SpringDamper(p, this.particleList[particleNum + Width - 1], this.springConstant, this.dampingFactor, this.restLength); // create a spring damper
                    this.springDamperList.Add(sd); // add that spring damper to the spring damper list
                    this.lineList.Add(this.CreateLineRenderers(sd)); // create a line renderer here and add it to the line list
                }
            }
        }

        /// <summary>
        /// Creates triangles for aerodynamic force.
        /// </summary>
        private void SetTriangles()  
        {
            // i = this particle
            // i + 1 = right
            // i + width = top
            // i + width - 1 = top left
            for (var i = 0; i < this.particleList.Count; i++)
            {
                if (i % Width != Width - 1 && i < (Width * Height) - Width) 
                {
                    // bottom left triangle
                    var tri = new Triangle(this.particleList[i], this.particleList[i + 1], this.particleList[i + Width]); // (bottom, right, top)
                    this.triangleList.Add(tri);
                }

                if (i % Width != 0 && i < (Width * Height) - Width)
                {
                    // top right triangle
                    var tri = new Triangle(this.particleList[i], this.particleList[i + Width - 1], this.particleList[i + Width]); // (bottom, top left, top)
                    this.triangleList.Add(tri);
                }
            }
        }

        /// <summary>
        /// Creates line renderers.
        /// </summary>
        /// <param name="sd">
        /// The spring damper.
        /// </param>
        /// <returns>
        /// The <see cref="GameObject"/>.
        /// </returns>
        private GameObject CreateLineRenderers(SpringDamper sd) 
        {
            // instantiate a line prefab
            var lineGo = Instantiate(this.LinePrefab, (sd.P1.Position + sd.P2.Position) / 2, new Quaternion()) as GameObject; // use line prefab to draw from first particle to second particle
            return lineGo;
        }
    }
}
