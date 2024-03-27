using UniRx;

public class PlayerRiches
{
    public ReactiveProperty<int> Coins { get; set; } = new();
    public ReactiveProperty<int> Diamonds { get; set; } = new();
}