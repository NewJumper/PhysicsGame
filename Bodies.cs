using System.Collections;
using System.Numerics;
using Raylib_cs;

namespace Physics;

public class Bodies {
    public static readonly List<PhysicsBody> BODIES = new List<PhysicsBody>();
    public static readonly PhysicsBody SUN = new PhysicsBody(new Vector2((Program.WindowSize.X - 20) / 2, (Program.WindowSize.Y - 20) / 2), 40);
    public static readonly PhysicsBody EARTH = new PhysicsBody(new Vector2((Program.WindowSize.X - 20) / 2 + 100, (Program.WindowSize.Y - 20) / 2), 10);

    public static void registerBodies() {
        SUN.SetProperties(100, Vector2.Zero, new Color(252, 186, 3, 255));
        EARTH.SetProperties(10, Vector2.Zero, new Color(11, 105, 219, 255));
        BODIES.Add(SUN);
        BODIES.Add(EARTH);
    }
}