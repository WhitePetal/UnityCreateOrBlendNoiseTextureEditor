# UnityCreateOrBlendNoiseTextureEditor
# Unity 程序生成或混合噪声纹理窗口编辑器
## 功能 ：
* 生成并保存噪声贴图。 目前可生成 PerlinNoise(柏林噪声) 和 WorelyNoise(沃利噪声) 这两种噪声的 2D 和 3D 贴图。
* 生成并保存“混合纹理”，可以生成这样一种 2D 或 3D 贴图，该贴图的 RGBA 四个通道，可以分别存储不同类型的噪声采样。

## 使用：

* 将 Noise 文件夹(最好连同其 .meta 文件) 放入到 Unity 的 Assets/Editor 目录下。
* 在 Unity 顶部选项栏中，选择 Tools/Noise/xx 即可选择具体要使用的噪声生成器。
* 设置 要生成的贴图的 名称(id), 长、宽、高、octave、f(生成噪声时的频率系数)、a(生成噪声时的振幅系数)
* 点击 窗口的 生成按钮，即可生成并保存噪声纹理。
>注意：
>在生产噪声贴图前，请确保你的 Unity 项目 有 Assets/Temp 该目录。 该目录用于存放生成出来的噪声贴图。 你也可以通过代码来修改该目录。

## 扩展性：
目前的功能仅有 PerlinNoise 、 WorelyNoise 和 “混合噪声” 这三种纹理的生成。
如果要添加新的 噪声，可以通过 以下步骤快速接入：

* 将新的噪声算法封装到一个类中，并让该类继承自 INoisCreater
* 创建一个脚本，让该脚本继承自 抽象类 SingleNoiseEditorWindowBase 

**INoisCreater 接口：**
 该接口有三个方法：
 float Get1D(float x)
 float Get2D(float x, float y)
 float Get3D(float x, float y, float z)
 分别对应着 噪声算法在 一维、二维、三维 坐标下 噪声值的计算
 >注意：噪声算法的返回值应当是 [0, 1] 的范围值！
 
**SingleNoiseEditorWindowBase 抽象类：**
 该抽象类作为 噪声纹理生成器 的基类
 你只需要让 新的噪声纹理生成器 继承自该类
 然后需要 重写 两个属性：
 * Creater： 该噪声生成器的 INoiseCreater 即上面提到的 实现了 INoiseCreater 接口的 噪声算法的实例化对象。
 * NoiseName： 和该噪声生成器生成的噪声类型名，即一个 string 类型，该名称会被用于计算生成的纹理的名字后缀。
 最后不要忘了：作为一个 EditorWindow 类，你还要自行实现 static Init() 方法，用于开启窗口。
 关于 OnGUI 部分，SingleNoiseEditorWindowBase 已经帮你实现完毕，不需要自行去 重写或调用。
