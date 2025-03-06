[English](./README.MD)


这个示例工程用于演示如何在 Unity 环境下使用 YVR Player，它基于 Unity2022.3.12f1 版本搭建。除了Unity内部库外，主要有以下依赖：

| 仓库名                | 版本号  | 简介                                                         |
| --------------------- | ------- | ------------------------------------------------------------ |
| YVR Player            | 1.3.2   | 基于ExoPlayer的视频播放库，相比Unity VideoPlayer提供更多格式的支持，更强的解码性能。 |
| YVR AndroidDeviceCore | 1.2.0   | 对AndroidJavaClass或JNI的集成，让Unity的C#脚本与安卓Java类交互时更加方便和安全。 |
| YVR UniRx             | 0.2.1   | 对UniRx做了小部分拓展。                                      |
| YVR Utilities         | 0.15.10 | Unity环境的常用、实用函数封装库，更方便的操作渲染线程和部分C#数据结构等。 |

## 如何将 YVR Player 接入到其他项目？

1. 参考项目中的packages.json，将YVR相关的包引入到项目中；

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

   

2. 在菜单栏选择"Edit/Project Settings..."打开项目设置页面，并切换到Player选项卡；

3. 保证 Other Settings 区域的选项是如下配置：

   1. Graphics APIs 的配置仅为 OpenGLES3；

   ![image-20250306135012688](.\README.ASSETS\image-20250306135012688.png)

   2. Minimum API Level为Android 10.0，Target API Level为34；

   ![image-20250306135106293](.\README.ASSETS\image-20250306135106293.png)

   3. Scripting Backend为IL2CPP，Target Architectures仅为ARM64；

   ![image-20250306135135194](.\README.ASSETS\image-20250306135135194.png)

   4. 参考SampleScene场景中的Player游戏对象，挂在YPlayer和配置它的输出材质，保证TargetMaterials中的材质与渲染视频的RawImage组件中的材质是同一个。

   ![image-20250306135757166](.\README.ASSETS\image-20250306135757166.png)

   ![image-20250306135824036](.\README.ASSETS\image-20250306135824036.png)

