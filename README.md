# ARGeoRecorder

https://user-images.githubusercontent.com/11372210/170836866-368213d9-225f-4d0c-a8b5-314fe388fd12.mp4

## About

ARCore Geospatial API に対応した ARSession を ARCore Recording & Playback API で記録・再生する Android アプリ。

## Tested Environment

- Unity 2021.3.4f1
- ARFoundation 4.2.7
- ARCore Extensions 1.35.0
- Android 12(Google Pixel 4a 5G)

## Settings & Usage

### Geospatial API 設定

Project Settings > ARCore Extensions > Optional Features から
Geospatial にチェックを入れます。

![img](https://storage.googleapis.com/zenn-user-upload/edefb5137292-20220513.png)

また、GCP で有効化した Android API Key を入力します。
Geospatial API の詳細な設定については[公式ドキュメント](https://developers.google.com/ar/develop/unity-arf/geospatial/developer-guide-android?hl=ja)をご覧ください。

### ビルド

Build Settings にて、プラットフォームが Android になっていない場合は切り替えて、ビルドをしてください。
`Assets\ARRecoreder\Scenes\main.unity`がメインのシーンになります。

### AR Session の記録

Recording を停止すると自動的にセッションをローカルファイルに記録します。
ファイルは Android 端末内の、`Android\data\com.drumath2237.ARRecorder\files`
に保存されます。

## Author

[にー兄さん / Kaito Tsutsumi](https://twitter.com/ninisan_drumath)

