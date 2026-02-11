using Godot;

public partial class Cam : Camera2D
{
    [Export] float ZoomIn = 1;
    static Cam TheCam;
    [Export] Font TheFont;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TheCam = this;
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
        // Godot.Collections.Array<CamPart> PotentialCams = [];
        // foreach (Node item in (GetParent() as User).Parts.GetChildren())
        // {
        // 	if (item.GetChild(0) is CamPart)
        // 	{
        // 		PotentialCams.Add(item.GetChild(0) as CamPart);
        // 	}
        // }
        // if (PotentialCams.Count > 0)
        // {
        // 	(GetParent() as User).TheCore.TargetCam = Mathf.Clamp((GetParent() as User).TheCore.TargetCam, 0, PotentialCams.Count - 1);
        // 	Position = PotentialCams[(GetParent() as User).TheCore.TargetCam].Body.Position;
        // }
        ZoomIn = Mathf.Clamp(ZoomIn - Input.GetAxis("Zoom Out", "Zoom In") * (float)delta, 0.0001f, 0.5f);
        Zoom = new(1 / ZoomIn, 1 / ZoomIn);
        GlobalPosition = ((GetParent() as User).TheCore.GetParent().GetParent() as Node2D).GlobalPosition;
    }
}
