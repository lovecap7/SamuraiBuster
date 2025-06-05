using UnityEngine;

// Ç¬Ç©ÇÌÇ»Ç¢ÇÃÇ‡é¿ëïÇµÇ»Ç¢Ç∆Ç¢ÇØÇ»Ç¢ÇÃÇ™Ç»Å[
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
