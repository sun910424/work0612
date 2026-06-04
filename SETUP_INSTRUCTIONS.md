# 石頭撿起系統 - 設置指南

## 📋 功能概述

這個系統包含：
1. **石頭物體** - 可交互的石頭 GameObject
2. **UI 提示** - 屏幕底部顯示 "按 F 撿起"
3. **F 鍵交互** - 按 F 後石頭消失

---

## 🛠️ 設置步驟

### 步驟 1：創建玩家 GameObject（如果還沒有）

1. 在 Hierarchy 中右鍵 → 3D Object → Capsule（或你的玩家角色）
2. 命名為 `Player`
3. **重要**：在 Inspector 中設置 Tag 為 `Player`
   - 點擊 Tag 下拉菜單 → Add Tag
   - 新增 Tag 名稱為 `Player`
   - 應用到玩家 GameObject

### 步驟 2：創建石頭 GameObject

1. 在 Hierarchy 中右鍵 → 3D Object → Cube
2. 命名為 `Rock`
3. 調整大小和位置（縮放為 0.5, 0.5, 0.5）
4. 添加 Collider：
   - 在 Inspector 中點擊 Add Component
   - 搜索 `BoxCollider` 並添加
   - **勾選 "Is Trigger"** ✓

### 步驟 3：添加 RockInteraction 腳本

1. 選擇 `Rock` GameObject
2. Add Component → 搜索 `RockInteraction`
3. 在 Inspector 中設置：
   - **Detection Range**: 3（檢測距離）
   - **Pickup Key**: F（撿起鍵）

### 步驟 4：創建 UI Canvas

1. 右鍵 Hierarchy → UI → Canvas
2. 命名為 `InteractionCanvas`
3. 在 Canvas 下創建 Text：
   - 右鍵 Canvas → UI → Text
   - 命名為 `PromptText`
   - 設置文字：`按 F 撿起`
   - 調整位置到屏幕底部中央
   - 調整字體大小（建議 36）

### 步驟 5：添加 InteractionUI 腳本

1. 選擇 `InteractionCanvas` GameObject
2. Add Component → 搜索 `InteractionUI`
3. 在 Inspector 中分配：
   - **Prompt Text**: 拖拽 `PromptText` 到此欄位
   - **Canvas Group**: 會自動添加或分配

### 步驟 6：連接 UI 到石頭

1. 選擇 `Rock` GameObject
2. 在 Inspector 中的 RockInteraction 組件：
   - 將 `InteractionUI` 欄位指向 Canvas 上的 InteractionUI 腳本

---

## 🎮 測試

1. **按下 Play 按鈕**
2. 移動玩家靠近石頭
3. 當接近時，屏幕底部會顯示 "按 F 撿起"
4. **按 F 鍵**，石頭消失 ✓

---

## 📐 調整參數

在 Inspector 中調整以下值：

- **Detection Range**（檢測範圍）：改變玩家要靠多近才能看到提示
- **Fade Duration**（淡入淡出時間）：改變 UI 出現/消失的速度
- 文字大小、位置、顏色等

---

## 🐛 常見問題

### 提示不出現？
- ✓ 確保玩家 GameObject 標籤為 `Player`
- ✓ 確保 InteractionUI 已分配到 RockInteraction
- ✓ 檢查 Canvas 和 PromptText 是否正確配置

### F 鍵不工作？
- ✓ 確保場景中有玩家
- ✓ 檢查 Pickup Key 是否設為 F
- ✓ 確保玩家在檢測範圍內

### 石頭的 Collider 不是 Trigger？
- ✓ 在 Stone 的 BoxCollider 中勾選 "Is Trigger"

---

## 📝 腳本文件位置

```
Assets/Scripts/
├── RockInteraction.cs    # 石頭交互邏輯
└── InteractionUI.cs      # UI 管理
```

---

**設置完成！現在可以遊戲了！** 🎉
