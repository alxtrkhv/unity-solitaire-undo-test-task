namespace Game.Solitaire
{
  public struct BoardSettings
  {
    public int TableauColumns { get; }
    public int FoundationPiles { get; }
    
    public BoardSettings(int tableauColumns = 7, int foundationPiles = 4)
    {
      TableauColumns = tableauColumns;
      FoundationPiles = foundationPiles;
    }
    
    public static BoardSettings Default => new(7, 4);
  }
}
