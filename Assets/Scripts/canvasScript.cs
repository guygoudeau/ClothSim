// --------------------------------------------------------------------------------------------------------------------
// <copyright file="canvasScript.cs" company="Guy Goudeau">
//   Property of Guy Goudeau, do not steal.
// </copyright>
// <summary>
//   Defines the Canvas type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assets.Scripts
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// The canvas script class.
    /// </summary>
    public class CanvasScript : MonoBehaviour
    {
        /// <summary>
        /// The spring constant.
        /// </summary>
        private readonly Slider springC;

        /// <summary>
        /// The damping factor.
        /// </summary>
        private readonly Slider dampF;

        /// <summary>
        /// The rest length.
        /// </summary>
        private readonly Slider restL;

        /// <summary>
        /// The gravity.
        /// </summary>
        private readonly Slider grav;

        /// <summary>
        /// The wind strength.
        /// </summary>
        private readonly Slider windS;

        /// <summary>
        /// The breaking factor.
        /// </summary>
        private readonly Slider breakF;

        /// <summary>
        /// The cloth simulation instance.
        /// </summary>
        private readonly ClothSim clothSim;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasScript"/> class.
        /// </summary>
        /// <param name="springC">
        /// The spring constant.
        /// </param>
        /// <param name="dampF">
        /// The damping factor.
        /// </param>
        /// <param name="restL">
        /// The rest length.
        /// </param>
        /// <param name="grav">
        /// The gravity.
        /// </param>
        /// <param name="windS">
        /// The wind strength.
        /// </param>
        /// <param name="breakF">
        /// The breaking factor.
        /// </param>
        /// <param name="clothSim">
        /// The cloth simulation instance.
        /// </param>
        public CanvasScript(Slider springC, Slider dampF, Slider restL, Slider grav, Slider windS, Slider breakF, ClothSim clothSim)
        {
            this.restL = restL;
            this.grav = grav;
            this.windS = windS;
            this.breakF = breakF;
            this.clothSim = clothSim;
            this.dampF = dampF;
            this.springC = springC;
        }

        /// <summary>
        /// Reloads the scene.
        /// </summary>
        /// <param name="sceneName">
        /// The name of the scene.
        /// </param>
        public void Reload(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Initializes starting values.
        /// </summary>
        private void Start()
        {
            this.springC.value = this.clothSim.springConstant;
            this.dampF.value = this.clothSim.dampingFactor;
            this.restL.value = this.clothSim.restLength;
            this.grav.value = this.clothSim.gravity;
            this.windS.value = this.clothSim.airVelocity;
            this.breakF.value = this.clothSim.breakingFactor;
        }

        /// <summary>
        /// Updates scene every frame.
        /// </summary>
        private void Update()
        {
            this.clothSim.springConstant = this.springC.value;
            this.clothSim.dampingFactor = this.dampF.value;
            this.clothSim.restLength = this.restL.value;
            this.clothSim.gravity = this.grav.value;
            this.clothSim.airVelocity = this.windS.value;
            this.clothSim.breakingFactor = this.breakF.value;
        }
    }
}
