# Quaternionについて

Quaternionたまに使うといつもよくわからなくなるので、
備忘録的にまとめてみました。
よく使う書き方だけメモ。



## よく使う回転系の処理

- transform.Rotateである軸を中心に指定角度分回転できる
	``` this.transform.Rotate(new Vector3(0,1,0),10);    // Y軸を回転軸にして１０度ずつ回転 ```
	
- transform.Rotateでワールド座標基準で、現在の回転量へ加算する
  ``` this.transform.Rotate(1.0f,1.0f,1.0f,Space.World); ```
  
- ゲームオブジェクトが向いている方向のベクトルを取得する
  	```this.transform.forward```
    これは以下と同じ意味になる
   
   ``` this.transform.rotation * Vector3.up```
   
   また、ワールド座標の指定した方向の単位ベクトルを取得したいときは以下のように書く
   
   ```
   var angles = new Vector3(0f, 45f, 0f); // Y軸を回転軸として45度回転した方向
   var direction = Quaternion.Euler(angles) * Vector3.forward;	
   ```
   
   


- Rotation同士をかけると２つの回転を加算したQuaternionを取得できる
  	
   今対象としているゲームオブジェクト(this)の向きに、上記で定義したdirectionの方向の角度を加算したい場合は以下のようにQuaternionをかけてあげるだけで方向を変えることができる。
   
	``` this.transform.rotation = Quaternion.LookRotation(direction) * this.transform.rotation; // directionは上記で作成した単位方向ベクトル  ```
  
  また、上記の方向を回転させる処理を一文で書くと以下のようになる。
  
	```
  this.transform.rotation = Quaternion.Euler(new Vector3(0f, 45f, 0f)) * this.transform.rotation;
	```


  	
- 時間をかけてゆっくりとゲームオブジェクトを指定の方向へ向かせる

   Sleapまたはleapを使うことでゆっくり回転できる。

	```
transform.rotation = Quaternion.Slerp(startPosition.rotation, targetDir, step);　// 現在のゲームオブジェクトの角度から目標の角度までゆっくりと回転させる
	```
	
	startPosition.rotation  :   初期の回転角度（だいたい対象のゲームオブジェクトの現在の角度）
	
	targetDir  :  目標の回転角度
	
	step   :   補完率(0～1) つまり0なら初期の角度　0.5なら初期と目標の角度の中間の角度まで回転



## サンプルプロジェクト内のデバッグキー操作

- Aキー　Y軸を回転軸にして１０度ずつ回転(Rotate関数)
- Bキー　ワールド座標基準で、現在の角度にRotation[1,1,1]を加算する(Rotate関数)
- Cキー　Y軸を回転軸にして90度回転した方向へゲームオブジェクトを回転
- Dキー　現在のゲームオブジェクトの角度にY軸を回転軸にして45度回転を追加する
- Eキー　ゲームオブジェクトをY軸を回転軸にして45度回転させる(Lerp関数)
- Xキー　ゆっくりゲームオブジェクトを指定の角度まで回転(Slerp関数)



## 参考資料

- forwardとはどういう計算をしているか
  - [https://qiita.com/FuwattoFlower/items/ae5be071ae3405ff70f8](https://qiita.com/FuwattoFlower/items/ae5be071ae3405ff70f8)
- Quaternion[同士をかけると回転させられるという説明]()
  - [http://tsubakit1.hateblo.jp/entry/2014/08/02/030919](http://tsubakit1.hateblo.jp/entry/2014/08/02/030919)
- LookAt関数について
  - [https://www.sejuku.net/blog/69635](https://www.sejuku.net/blog/69635)
- Quaternionの使い方全般
  - [https://www.f-sp.com/entry/2017/08/30/171353](https://www.f-sp.com/entry/2017/08/30/171353)
- 二点間角度取得＆ゆっくり回す
  - [https://yowabi.blogspot.com/2018/06/unity.html](https://yowabi.blogspot.com/2018/06/unity.html)
- transform.Rotateについて
  - [https://www.sejuku.net/blog/51521](https://www.sejuku.net/blog/51521)
- Quaternion復習
  - [https://qiita.com/r-ngtm/items/ddf418b13d4b9d767403](https://qiita.com/r-ngtm/items/ddf418b13d4b9d767403)

