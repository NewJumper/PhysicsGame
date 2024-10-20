using System.Numerics;
using Raylib_cs;

namespace Physics;

public class Program {
    public static readonly Vector2 WindowSize = new(1280, 720);
    public static readonly Color WindowBgColor = Color.Black;
    public static PhysicsBody selected;

    public static void Main() {
        Raylib.InitWindow((int)WindowSize.X, (int)WindowSize.Y, "test");
        Raylib.SetTargetFPS(60);

        Bodies.registerBodies();
        selected = Bodies.EARTH;

        Vector2 clickedPos = new(0, 0);
        bool tracking = false;

        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(WindowBgColor);
            
            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                clickedPos = Raylib.GetMousePosition();

                foreach (PhysicsBody body in Bodies.BODIES) {
                    if (body.Hovering(clickedPos)) {
                        selected = body;
                        break;
                    }
                }
                
                tracking = true;
            }

            float power = 0;
            Vector2 direction = Vector2.Zero;
            if (tracking) {
                Vector2 mousePos = Raylib.GetMousePosition();
                power = Vector2.Distance(clickedPos, mousePos);
                direction = new Vector2(clickedPos.X - mousePos.X, clickedPos.Y - mousePos.Y);
                if(direction.Length() != 0) direction = Vector2.Normalize(direction);
                selected.VisualizeForce(power * direction);
            }

            if (Raylib.IsMouseButtonReleased(MouseButton.Left)) {
                Vector2 force = power * direction;
                force.X -= force.X % 10;
                force.Y -= force.Y % 10;
                selected.AppleForce(force);
                tracking = false;
            }

            int index = Bodies.BODIES.IndexOf(selected);
            switch (Raylib.GetKeyPressed()) {
                case (int)KeyboardKey.W:
                    selected.AppleForce(new Vector2(0, -10)); break;
                case (int)KeyboardKey.A:
                    selected.AppleForce(new Vector2(-10, 0)); break;
                case (int)KeyboardKey.S:
                    selected.AppleForce(new Vector2(0, 10)); break;
                case (int)KeyboardKey.D:
                    selected.AppleForce(new Vector2(10, 0)); break;
                case (int)KeyboardKey.Space:
                    selected.AppleForce(-selected.GetVelocity()); break;
                case (int)KeyboardKey.LeftBracket:
                    index = (index - 1) % Bodies.BODIES.Count; break;
                case (int)KeyboardKey.RightBracket:
                    index = (index + 1) % Bodies.BODIES.Count; break;
            }
            index = index < 0 ? Bodies.BODIES.Count + index : index;
            selected = Bodies.BODIES[index];

            foreach (PhysicsBody body in Bodies.BODIES) {
                body.Draw();
            }

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
