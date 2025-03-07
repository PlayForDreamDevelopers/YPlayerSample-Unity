[![en](https://img.shields.io/badge/lang-en-blue.svg)](.\README.md)

这个示例工程用于演示如何在 Unity 环境下使用 YVR Player，它基于 Unity2022.3.52f1 版本搭建。除了Unity内部库外，主要有以下依赖：

1. [YVR Player](https://github.com/PlayForDreamDevelopers/com.yvr.player-mirror)
2. [YVR AndroidDeviceCore](https://github.com/PlayForDreamDevelopers/com.yvr.android-device.core-mirror)
3. [YVR UniRx](https://github.com/PlayForDreamDevelopers/com.yvr.unirx-mirror)
4. [YVR Utilities](https://github.com/PlayForDreamDevelopers/com.yvr.utilities-mirror)

示例内提供了在线视频、本地视频的播放控制和使用方法，并提供了左右分割、上下分割的3D视差视频和180°、360°全景视频的应用示例。

想添加本地测试视频，可以放到"\Android\data\com.yvr.playerdemo\files\videos"目录中。

推荐的在线视频地址: https://gist.github.com/jsturgis/3b19447b304616f18657

## 如何将 YVR Player 接入到其他项目？

1. 参考项目中的 packages.json ，将 YVR 相关的包引入到项目中；

   ```json
   {
     "dependencies": {
   ...
       "com.yvr.android-device.core": "git@github.com:PlayForDreamDevelopers/com.yvr.android-device.core-mirror.git?path=/com.yvr.android-device.core#0540b2af10a4d83e40f3b62b457a5bb6e742e9b6",
       "com.yvr.json-parser": "git@github.com:PlayForDreamDevelopers/com.yvr.json-parser-mirror.git?path=/com.yvr.json-parser#87438d1a077e9b648dc5393637174f33aaefe104",
       "com.yvr.player": "git@github.com:PlayForDreamDevelopers/com.yvr.player-mirror.git?path=/com.yvr.player#051858c079647678aea66a910e7150a54a1179b9",
       "com.yvr.unirx": "git@github.com:PlayForDreamDevelopers/com.yvr.unirx-mirror.git?path=/com.yvr.unirx#a795fe53c94d3761f6bad216c05bb480b926bc8f",
       "com.yvr.utilities": "git@github.com:PlayForDreamDevelopers/com.yvr.utilities-mirror.git?path=/com.yvr.utilities#c06beb422cd7e07324aa50ec3196be2ef1de1205",
   ...
     }
   }
   
   ```

2. 在菜单栏选择 "Edit/Project Settings..." 打开项目设置页面，并切换到 Player 选项卡；

3. 保证 Other Settings 区域的选项是如下配置：

   1. 确保项目按照 [Get Started (XR)](https://developer.pfdm.cn/yvrdoc/unity/UserManual/GetStartedXR.html) 进行了配置；

   2. 由于 ExoPlayer 的运行环境要求，需要保证 Minimum API Level为Android 10.0，Target API Level为34；

   ![image-20250306135106293](.\README.ASSETS\image-20250306135106293.png)

   3. 参考 SampleScene 场景中的 Player 游戏对象，挂载 YPlayer 和配置它的输出材质，保证 TargetMaterials 中的材质与渲染视频的 RawImage 组件中的材质是同一个。