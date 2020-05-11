using UnityEngine;

public abstract class EditorController : ScriptableObject {
    private MusicEditor musicEditor;

    public EditorPointerPosChanged PointerPosChanged => musicEditor.PointerPosChanged;

    protected void Init(MusicEditor musicEditor) {
        this.musicEditor = musicEditor;
    }

    public void StartPointerControl() {
        musicEditor.SavePointerPos();
    }

    public void MoveControlledPointer(int pos) {
        musicEditor.MovePointerTo(pos);
    }

    public void EndPointerControl() {
        musicEditor.RestorePointerPos();
    }

    public abstract void Destroy();
}