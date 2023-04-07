# Algorithms

# 目標  
- 学習目標  
ゲームでつかわれている様々なアルゴリズムを理解する（余力があれば他のアルゴリズムも）
- 制作目標  
アルゴリズムを使ったゲームを作る  

# 結果　
## 穴掘り法  
スクリプト：https://github.com/TsuyoshiUsugi/Algorithms/blob/master/Assets/Scripts/%E7%A9%B4%E6%8E%98%E3%82%8A/MazeDig2.cs  
1 最初にエリアのサイズを決める。この時、縦横のサイズは5以上でかつ奇数でなくてはならない。  
2 エリアの全域を壁に設定する。  
3 エリアの端の部分を道にする。これはこのあとの道を生成するときに端っこまで道を伸ばさないようにするためである。  
4 最初に掘る地点を決める。この時座標はx,yともに奇数でなくてはならない。これは外壁になる壁は縦or横のマスが偶数になるためである。これにより外壁には穴を掘らなくなる。  
5 穴を掘る。  
具体的には最初に与えられた地点から、上下左右2マスが壁であるか確認する。そこから進める方向をランダムに選び、通路にする。  
これを掘れなくなるまでやる。  
6 掘り進めた結果四方のどこにも進めなくなった場合、これまで掘ってきたなかで奇数の座標をランダムに取得し、5を繰り返す。  
### 感想 
区域分割法に挫折して簡単そうとおもってやってみたが、所々難しいところがあって大変だった。しかしアルゴリズムをUnityでつかうことが出来るという経験をできたのはよかった。

# メモ  

# 進捗  

# 参考
- https://tourmaline-resonance-078.notion.site/6eeff072a3ad424f83f3b808bf227454  
- https://algoful.com/Archive/Algorithm/MazeDig
