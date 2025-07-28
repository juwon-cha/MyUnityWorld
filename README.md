# MyUnityWorld

**Unity Editor Version:** 2022.3.17f1

## 프로젝트 개요

MyUnityWorld는 다양한 상호작용과 미니게임을 즐길 수 있는 2D 롤플레잉 게임이다. 플레이어는 자신의 캐릭터를 커스터마이징하고, 월드를 탐험하며 NPC와 대화하고 다양한 미니게임을 즐길 수 있다.

## 플레이 예시



## 프로젝트 구조

```
MyUnityWorld/
├── Assets/
│   ├── Animations/       # 애니메이션 파일
│   ├── Artwork/          # 아트워크 및 스프라이트
│   ├── Externals/        # 외부 에셋
│   ├── Input/            # 입력 시스템 설정
│   ├── Prefabs/          # 프리팹
│   ├── Scenes/           # 게임 씬
│   ├── Scripts/          # C# 스크립트
│   │   ├── MyUnityWorld/ # 메인 게임 로직
│   │   │   ├── Dialogue/ # 대화 시스템 관련 스크립트
│   │   │   ├── EventTrigger/ # 상호작용 관련 스크립트
│   │   │   ├── GameData/ # 게임 데이터 관련 스크립트
│   │   │   ├── Manager/  # 매니저 관련 스크립트
│   │   │   ├── Player/   # 플레이어 관련 스크립트
│   │   │   └── UI/       # UI 관련 스크립트
│   │   ├── FlappyPlane/    # Flappy Plane 미니게임 스크립트
│   │   └── TopDownShooting/ # Top-Down Shooting 미니게임 스크립트
│   └── ...
├── Packages/             # 패키지 매니저 설정
└── ProjectSettings/      # 프로젝트 설정
```

## 설치 및 실행 방법

1. Unity Hub를 실행한다.
2. "프로젝트 열기"를 선택하고 이 프로젝트의 루트 폴더를 선택한다.
3. Unity 에디터가 열리면 `Assets/Scenes/MyUnityWorld.unity` 씬을 실행한다.

## 주요 기능 및 구현 방식

### 1. 캐릭터
- **이동:**
    - `PlayerController.cs`에서 `Input System`을 사용해 플레이어의 입력을 받아 `Rigidbody2D.velocity`를 조절하는 방식으로 이동을 구현했다.
    - 이동 방향에 따라 `transform.localScale`을 변경하여 캐릭터의 좌우 방향 전환을 구현한다.
- **커스터마이징:**
    - **색상:** `CustomizeSelectUI.cs`에서 색상 버튼을 클릭하면 `PlayerController.ChangeColor()`를 호출하여 `SpriteRenderer`의 `color` 속성을 변경한다.
    - **장비:** `EquipmentHandler.cs`와 `CustomizeSelectUI.cs`를 통해 장비 변경을 관리한다. 장비 버튼 클릭 시 `PlayerController.ChangeEquipment()`를 호출하여 `SpriteRenderer`의 `sprite`를 변경하고 활성화한다.
- **상호작용:**
    - `InteractionManager.cs`가 플레이어와 상호작용 가능한 오브젝트 간의 중재자 역할을 한다.
    - `TriggerDetection.cs`가 `Collider2D`를 통해 상호작용 가능한 오브젝트를 감지하고, `InteractionManager`에 알린다.
    - `PlayerController`는 `OnInteractPressed` 이벤트를 통해 상호작용 입력을 `InteractionManager`에 전달한다.

### 2. 월드
- **맵 디자인:**
    - `Tilemap`을 사용하여 맵을 제작했다.
    - 각 맵에는 `Collider2D`가 포함된 오브젝트를 배치하여 이벤트 트리거로 활용한다.
- **카메라:**
    - `FollowCamera.cs`에서 `LateUpdate()`를 사용해 플레이어의 움직임을 부드럽게 따라가도록 구현했다.
    - `Mathf.Lerp`를 사용해 부드러운 카메라 이동을 구현하고, `Tilemap`의 경계를 계산하여 카메라 이동 범위를 제한한다.

### 3. 게임 시스템
- **미니게임:**
    - `MiniGameEvent.cs`에서 `SceneManager.LoadScene()`을 호출하여 각 미니게임 씬을 로드하는 방식으로 구현했다.
    - `TriggerDetection`을 통해 특정 위치에 도달하면 상호작용을 통해 미니게임이 시작된다.
- **리더보드:**
    - `LeaderBoardUI.cs`와 `ScoreBoardUI.cs`를 통해 리더보드 UI를 구현했다.
    - `GameData.cs`에 각 미니게임의 최고 점수를 저장하고, `LeaderBoardUI`에서 이를 불러와 표시한다.
- **탑승물:**
    - `RideHandler.cs`와 `CustomizeSelectUI.cs`를 통해 탑승물 변경을 관리한다.
    - 각 탑승물은 `RideHandler.cs`에 정의된 고유한 속도 값을 가지며, `PlayerController.ChangeRide()`를 통해 플레이어의 기본 속도에 이 값이 더해진다.
    - 탑승물 선택 시 `PlayerController.ChangeRide()`를 호출하여 플레이어의 `Speed`를 증가시키고, `SpriteRenderer`를 활성화하여 탑승물 이미지를 표시한다.
- **NPC 및 대화:**
    - `DialogueHandler.cs`에서 대화 내용을 관리하고, 타자기 효과(`Typing Effect`)를 구현했다.
    - `InteractionManager`를 통해 플레이어가 NPC와 상호작용하면 `DialogueHandler`의 `StartDialogue()`를 호출하여 대화를 시작한다.
    - 대화 중 'F' 키를 누르면 `HandleInteraction()`을 통해 대화를 스킵하거나 종료할 수 있다.
    - 대화가 표시되거나 완료된 상태에서 'Enter' 키를 누르면 `InteractionManager.EnterEvent()`가 호출되어 `BaseEvent`를 상속받는 다양한 이벤트(미니게임 시작, 커스터마이징 UI 열기 등)를 실행한다.
- **저장 및 로드:**
    - `GameManager.cs`와 `GameData.cs`를 통해 게임 데이터를 관리한다.
    - `PlayerPrefs`를 사용하여 플레이어의 마지막 위치, 커스터마이징 선택(색상, 장비, 탑승물), 미니게임 최고 점수를 저장한다.
    - 게임 시작 시 `GameManager.OnEnable()`에서 `PlayerPrefs`에 저장된 데이터를 불러와 게임 상태를 복원한다.

