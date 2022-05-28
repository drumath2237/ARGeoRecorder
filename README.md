# ARGeoRecorder

https://user-images.githubusercontent.com/11372210/170836866-368213d9-225f-4d0c-a8b5-314fe388fd12.mp4

## About

ARCore Geospatial API に対応した ARSession を ARCore Recording & Playback API で記録・再生する Android アプリ。

## Tested Environment

- Unity 2021.3.2f1
- ARFoundation 4.2.3
- ARCore Extensions 1.31.0
- Android 12(Google Pixel 4a 5G)

## Settings & Usage

### Geospatial API設定

Project Settings > ARCore Extensions > Optional Featuresから
Geospatialにチェックを入れます。

![img](https://storage.googleapis.com/zenn-user-upload/edefb5137292-20220513.png)

また、GCPで有効化したAndroid API Keyを入力します。
Geospatial APIの詳細な設定については[公式ドキュメント](https://developers.google.com/ar/develop/unity-arf/geospatial/developer-guide-android?hl=ja)をご覧ください。

### ビルド

Build Settingsにて、プラットフォームがAndroidになっていない場合は切り替えて、ビルドをしてください。
`Assets\ARRecoreder\Scenes\main.unity`がメインのシーンになります。


## Contact

何かございましたら、[にー兄さんのTwitter](https://twitter.com/ninisan_drumath)
までご連絡ください。
