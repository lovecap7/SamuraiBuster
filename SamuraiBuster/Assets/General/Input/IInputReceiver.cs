using UnityEngine;

// つかわないのも実装しないといけないのがなー
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
    void ReleaceSkill();
    void Move(Vector2 axis);
}
