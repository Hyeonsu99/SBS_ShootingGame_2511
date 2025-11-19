using UnityEngine;

public interface IManager
{
    void GameInitialize(); // 게임 초기화
    void GameStart(); // 게임 시작
    void GamePause(); // 일시 정지
    void GameResume(); // 게임 재개
    void GameOver(); // 게임 종료
    void GameTick(float delta); // 업데이트
}
