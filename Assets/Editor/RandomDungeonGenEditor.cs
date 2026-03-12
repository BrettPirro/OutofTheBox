using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGen),true)]
public class RandomDungeonGenEditor : Editor
{
    AbstractDungeonGen generator;

    private void Awake()
    {
        generator = (AbstractDungeonGen)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Gen Dungeon")) { generator.GenDungeon(); }
    }

}
