using UnityEngine;

// ����Ȃ��̂��������Ȃ��Ƃ����Ȃ��̂��ȁ[
public interface IInputReceiver
{
    void Submit();
    void Cancel();
    void TriggerUp();
    void TriggerDown();
    void TriggerRight();
    void TriggerLeft();
    void Attack();
    void Skill();
    void Move(Vector2 axis);
}
