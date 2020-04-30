# Messager  
UniRx Messager for gameloop  

充分利用多核多线程CPU的性能，榨干设备的机能，发挥最好的游戏效果  
理想状态：秒读条，异步加载资源，掉帧可能性降到最低  
  
主要解决当前游戏中的问题：  
1.异步资源，Addressable Ready -> Async Memory load -> Show  
2.异步本地消息，让代码调度到线程池里面，减少主线程的负担  
