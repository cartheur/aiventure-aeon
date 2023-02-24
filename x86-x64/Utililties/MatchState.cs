namespace Animals.Core.Utililties
{
    /// <summary>
    /// Denotes what part of the input path a node represents.
    /// 
    /// Used when pushing values represented by wildcards onto collections for
    /// the star, thatstar and topicstar  values.
    /// </summary>
    public enum MatchState
    {
        UserInput,
        That,
        Topic
    }
}
