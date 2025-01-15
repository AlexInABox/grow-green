using Godot;

public partial class DragManager : Node
{
    public static bool IsDragging { get; private set; }
    public static Plant DraggedPlant { get; private set; }

    public static void StartDragging(Plant plant)
    {
        IsDragging = true;
        DraggedPlant = plant;
    }

    public static void StopDragging()
    {
        IsDragging = false;
        DraggedPlant = null;
    }
}