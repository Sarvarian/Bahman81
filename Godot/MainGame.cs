using Godot;
using Survival.nodes.no_use_in_editor;

namespace Survival;

public partial class MainGame : Node2D
{
    public override void _Ready()
    {
        var input = InputHandler.Instantiate(this);
        StartGame(input);
    }

    private void StartGame(InputHandler input)
    {
        GameNode.Instantiate(this, input);
    }

}