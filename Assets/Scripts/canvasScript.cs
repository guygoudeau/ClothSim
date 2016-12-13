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
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Slider springC;

        /// <summary>
        /// The damping factor.
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Slider dampF;

        /// <summary>
        /// The rest length.
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Slider restL;

        /// <summary>
        /// The gravity.
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Slider grav;

        /// <summary>
        /// The wind strength.
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Slider windS;

        /// <summary>
        /// The cloth simulation instance.
        /// </summary>
        [SerializeField]
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private ClothSim clothSim;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasScript"/> class.
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
        /// <param name="wS">
        /// The wind strength.
        /// </param>
        /// <param name="cS">
        /// The cloth simulation instance.
        /// </param>
        public CanvasScript(Slider sC, Slider dF, Slider rL, Slider g, Slider wS, ClothSim cS)
        {
            this.restL = rL;
            this.grav = g;
            this.windS = wS;
            this.clothSim = cS;
            this.dampF = dF;
            this.springC = sC;
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
        // ReSharper disable once UnusedMember.Local
        private void Start()
        {
            this.springC.value = this.clothSim.SpringConstant;
            this.dampF.value = this.clothSim.DampingFactor;
            this.restL.value = this.clothSim.RestLength;
            this.grav.value = this.clothSim.Gravity;
            this.windS.value = this.clothSim.AirVelocity;
        }

        /// <summary>
        /// Updates scene every frame.
        /// </summary>
        // ReSharper disable once UnusedMember.Local
        private void Update()
        {
            this.clothSim.SpringConstant = this.springC.value;
            this.clothSim.DampingFactor = this.dampF.value;
            this.clothSim.RestLength = this.restL.value;
            this.clothSim.Gravity = this.grav.value;
            this.clothSim.AirVelocity = this.windS.value;
        }
    }
}