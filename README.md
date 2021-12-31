# EncryptString

## 概要
パスワードを指定して、文字列を暗号化、又は複合します。

## 対象のバージョン
.NETFramework 4.6.2

## 引数
| 引数 | 値 | 説明 |
|-|-|-|
| 第一引数 | "/e" or "/d"　| /e ・・・ 暗号化、/d ・・・ 複合 |
| 第二引数 | 対象の文字列 | /e の場合・・・暗号化したい文字列、<br>/dの場合 ・・・ 暗号化済みテキスト
| 第三引数 | パスワード | 暗号化と複合には同じパスワードを指定 |

## 使用例
暗号化する場合

```
EncryptString.exe /e password ABCD
y0wdJwH2WRAMZuuyuTArPA==
```

複合する場合

```
EncryptString.exe /d y0wdJwH2WRAMZuuyuTArPA== ABCD
password
```


暗号化後の文字列または複合した文字列が、標準出力(StdOut)に出力されます。

## 更新履歴
- 2021/05/19 Ver 1.1 末尾に改行コードが付与されないように変更
- 2021/05/18 Ver 1.0 初回リリース
