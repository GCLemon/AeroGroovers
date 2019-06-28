namespace AeroGroovers.Model
{
    /// <summary>
    /// コントローラークラスに継承するクラス
    /// </summary>
    public abstract class Controller
    {
        /// <summary>
        /// コントローラーに割り当てるID
        /// </summary>
        public int ControllerID { get; protected set; }

        public Controller(int id) => ControllerID = id;
    }
}
