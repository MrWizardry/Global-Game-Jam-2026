using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GuardManager))]
public class GuardManagerEditor : Editor
{
    private void OnSceneGUI()
    {
        GuardManager guard = (GuardManager)target;

        Color c = Color.yellow;
        if (guard.alertStage == AlertStage.Investigando)
            c = Color.Lerp(Color.yellow, Color.red, guard.alertLevel / 200f); // Alterar cor e tempo de detecção visualmente
        else if (guard.alertStage == AlertStage.Alerta)
            c = Color.red;

        Handles.color = new Color(c.r, c.g, c.b, 0.3f);
        Handles.DrawSolidArc(
            guard.transform.position,
            guard.transform.up,
            Quaternion.AngleAxis(-guard.fovAngle / 2f, guard.transform.up) * guard.transform.forward,
            guard.fovAngle,
            guard.fov);

        Handles.color = c;
        guard.fov = Handles.ScaleValueHandle(
            guard.fov,
            guard.transform.position + guard.transform.forward * guard.fov,
            guard.transform.rotation,
            3,
            Handles.SphereHandleCap,
            1);
    }
}
