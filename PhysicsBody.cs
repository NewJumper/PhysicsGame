using System.Numerics;
using Raylib_cs;

namespace Physics;

public class PhysicsBody {
    private Vector2 position;
    private float radius;
    private float mass;
    private Vector2 velocity;
    private Color color;

    public PhysicsBody(Vector2 position) : this(position, 20) { }
    
    public PhysicsBody(Vector2 position, float radius) {
        this.position = position;
        this.radius = radius;
        this.mass = 0;
        this.velocity = Vector2.Zero;
        this.color = Color.Black;
    }

    public void SetProperties(float mass, Vector2 velocity, Color color) {
        this.mass = mass;
        this.velocity = velocity;
        this.color = color;
    }

    public void VisualizeForce(Vector2 force) {
        Raylib.DrawLineV(position, position + force, color);
        Raylib.DrawCircleV(position, radius + 5, Program.WindowBgColor);
    }

    public void AppleForce(Vector2 force) {
        this.velocity += force;
    }

    public void Update() {
        position += velocity * Raylib.GetFrameTime();
        if (position.X - radius < 0 || position.X + radius > Program.WindowSize.X) velocity = new Vector2(velocity.X * -1, velocity.Y);
        if (position.Y - radius < 0 || position.Y + radius > Program.WindowSize.Y) velocity = new Vector2(velocity.X, velocity.Y * -1);
    }

    public void Draw() {
        Update();

        if (Program.selected == this) {
            Raylib.DrawCircleV(position, radius + 3, color);
            Raylib.DrawCircleV(position, radius + 2, Program.WindowBgColor);
        }
        Raylib.DrawCircleV(position, radius, color);
    }

    public Vector2 GetVelocity() {
        return velocity;
    }

    public bool Hovering(Vector2 mousePosition) {
        return Math.Pow(mousePosition.X - position.X, 2) + Math.Pow(mousePosition.Y - position.Y, 2) <= radius * radius;
    }
}