using System.Numerics;
using Raylib_cs;

namespace Physics;

public class Program {
    public static readonly Vector2 WindowSize = new(1280, 720);
    public static readonly Color WindowBgColor = Color.White;

    public static void Main() {
        Raylib.InitWindow((int)WindowSize.X, (int)WindowSize.Y, "test");
        Raylib.SetTargetFPS(60);

        PhysicsBody body1 = new PhysicsBody(new Vector2(100, (int)WindowSize.Y - 100));

        Vector2 clickedPos = new(0, 0);
        bool tracking = false;

        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(WindowBgColor);
            
            if (Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                clickedPos = Raylib.GetMousePosition();
                tracking = true;
            }

            float power = 0;
            Vector2 direction = Vector2.Zero;
            if (tracking) {
                Vector2 mousePos = Raylib.GetMousePosition();
                power = Vector2.Distance(clickedPos, mousePos);
                direction = new Vector2(clickedPos.X - mousePos.X, clickedPos.Y - mousePos.Y);
                if(direction.Length() != 0) direction = Vector2.Normalize(direction);
                body1.VisualizeForce(power * direction);
            }

            if (Raylib.IsMouseButtonReleased(MouseButton.Left)) {
                body1.AppleForce(power * direction);
                tracking = false;
            }

            body1.Draw();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
