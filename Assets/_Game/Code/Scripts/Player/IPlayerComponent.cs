namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Interfejs umożliwiający szybkie wyszukiwanie komponentów gracza i inicjalizowanie ich
    /// </summary>
    interface IPlayerComponent
    {
        public void Initialize(PlayerController playerController);
    }
}