# Safester.CryptoLibrary

## Open PGP C# PCL Library

## v1.0.2 - January 2019, 12

<img src="https://www.safester.net/img/icon-64x64.png" alt="Safester Icon"/>

# Fundamentals 

Safester.CryptoLibrary is a simple to use encryption library based on [OpenPGP](https://www.openpgp.org/) and packaged as a PCL.

It provides :

- PGP key pair generations,
- files and texts encryption & decryption.

The scope of library is voluntarily limited to encryption operations only, for ease of use.

## Technical operating environment – Portable Class Library

The library is entirely written in C# and is packaged as a Portable Class Library, which works on Windows Desktop and on Android, iOS & macOS with Xamarin.

The targets of the library are:

- NetFramework 4.5
- ASP.NETCore 1.0
- Windows7+
- Xamarin.Android
- Xamarin.iOS/ Xamarin.iOS Classic
- Xamarin.Mac

The underlying crypto library used is Bouncy Castle through it's PCL implementation.

## License

The SDK is licensed with the liberal [Apache 2.0](https://www.apache.org/licenses/LICENSE-2.0) license.

## Installation

Install the [NuGet Package](https://www.nuget.org/packages/Safester.CryptoLibrary/).

# Using the Library

## Generating Key Pair

Key Pair generation is done with the `PgpKeyPairGenerator`class:

```C#
string identity = "john@smith.com";
char [] passphrase = "my_passphrase".ToCharArray();

PgpKeyPairGenerator pgpKeyPairGenerator = 
    new PgpKeyPairGenerator(identity, passphrase, PublicKeyAlgorithm.DSA_ELGAMAL, PublicKeyLength.BITS_1024);
```

It is then possible to retrieve the Base64 armored PGP keyrings for crypto operations:

```C#
String privateKeyring = pgpKeyPairHolder.PrivateKeyRing;
String publicKeyring = pgpKeyPairHolder.PublicKeyRing;
```
This is and example of the display of the the two keyrings:

```pgp
-----BEGIN PGP PRIVATE KEY BLOCK-----
Version: BCPG C# v1.8.4.0

lQHpBFw6TdMRBADjBegAlO3Qq6RiNrAXBWHAbaQuSvUNSddZiyU+IJqj5Rdzxk6w
KhMw16XHzv/OZqKhEdW/AN3+y5pJAjyUIoCQuXQUVeMp8ND2mHMLmFFy0kjmeAiL
W/i4sWH4T57C4Sj/298/cH5GwYNIvReGqD3ZILGcDXqK2ZNyVy98iRcI/QCg+iYU
EkUGBR3ZWtBHdBYgluhumNUD/iYUvDSEEasM3YftGNn3PuIhotGjXsqmV1UkSDUM
OOUftBaLGOVkrlJj3DzUQpvs7Jk/JqqA6gguA97TX19zHyh3cMko8oa3iqdYlbju
q59FNDzFCP/KuBtQzG7OULkJ3uZMInEnwQu0o0p1/BJUOKIBrUMs3jwL6rH+2wjb
JQkEA/sHDLvYs6WwXOA7dOybkHkfeKHrGvP0TpkoCqqz4Ma2mFetyHqQ2eYfndYp
fYJj63g5f/W8tHgwujbXfyCTH+DBaRRk3sJqmiZt/LTeUHXIngECswiPnUmIovwF
KjjnxxJSUo81n3w6YRPCQ8ZtbyJ8G0dV6v3GXxHclK3nKH+b1P4JAwLFr2EtBbiC
92C+LOhR80hgeapWOZED0JeM+aVO+xC+Pg6xOsR66UoQavA8QKJWhgyxDpVcBT4u
289KiHIuOf7cVaeltA5qb2huQHNtaXRoLmNvbYhGBBMRAgAGBQJcOk3TAAoJEObA
Pln68s0Sa1oAoPBHDibuWjKe74FS630An330P1hmAJsE6fXkjOOgMO2Dv/OMDqRy
t8yyK50BPwRcOk3TEAIAlJT+wJXzuF7ihlQrODb8gaXdCgNJtMI53Th0TUiM+OMd
uLy30ztBq7nlozzKkUSxzvMyyUvwVzvwR6OsqYzfOwH9FT1dYXKttDBFtoro4d4Q
cLYTcAVobSnT1zp3SRmWge5bISyblr/c+lsgzV4/0gRIldYJz5tBC3oPEsocuaQo
zAH/dJElsiRkoM8Yec4iHnw0RLkDAXsr61l7Ik2PIfBNtVHJV/XWu2gfmeRyG3g6
D0Zludwgngl+m7bKlCSy6VhhH/4JAwLvHcEREo4jWmBw5/mZk/idvZsTr4uXIujx
DMs6+j8Enp66OYPgkYb+ogDmG4eUlWNz0KT8vTLFzHax/zP3EBD6wxyxNbDzUeqA
raCbYgl5wzU45B1UzhsK8e9UTdDcjBECgxjwrbnzn5SRUAVYzJSIRgQYEQIABgUC
XDpN0wAKCRDmwD5Z+vLNEkRkAJ9thlwACGja4xMCpBq6BGngY988qwCeNXR1IjC3
HEqBNumAbdL5lUHX8XU=
=2N1Y
-----END PGP PRIVATE KEY BLOCK-----

```

```pgp
-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: BCPG C# v1.8.4.0

mQGiBFw6TdMRBADjBegAlO3Qq6RiNrAXBWHAbaQuSvUNSddZiyU+IJqj5Rdzxk6w
KhMw16XHzv/OZqKhEdW/AN3+y5pJAjyUIoCQuXQUVeMp8ND2mHMLmFFy0kjmeAiL
W/i4sWH4T57C4Sj/298/cH5GwYNIvReGqD3ZILGcDXqK2ZNyVy98iRcI/QCg+iYU
EkUGBR3ZWtBHdBYgluhumNUD/iYUvDSEEasM3YftGNn3PuIhotGjXsqmV1UkSDUM
OOUftBaLGOVkrlJj3DzUQpvs7Jk/JqqA6gguA97TX19zHyh3cMko8oa3iqdYlbju
q59FNDzFCP/KuBtQzG7OULkJ3uZMInEnwQu0o0p1/BJUOKIBrUMs3jwL6rH+2wjb
JQkEA/sHDLvYs6WwXOA7dOybkHkfeKHrGvP0TpkoCqqz4Ma2mFetyHqQ2eYfndYp
fYJj63g5f/W8tHgwujbXfyCTH+DBaRRk3sJqmiZt/LTeUHXIngECswiPnUmIovwF
KjjnxxJSUo81n3w6YRPCQ8ZtbyJ8G0dV6v3GXxHclK3nKH+b1LQOam9obkBzbWl0
aC5jb22IRgQTEQIABgUCXDpN0wAKCRDmwD5Z+vLNEmtaAKDwRw4m7loynu+BUut9
AJ999D9YZgCbBOn15IzjoDDtg7/zjA6kcrfMsiu4zARcOk3TEAIAlJT+wJXzuF7i
hlQrODb8gaXdCgNJtMI53Th0TUiM+OMduLy30ztBq7nlozzKkUSxzvMyyUvwVzvw
R6OsqYzfOwH9FT1dYXKttDBFtoro4d4QcLYTcAVobSnT1zp3SRmWge5bISyblr/c
+lsgzV4/0gRIldYJz5tBC3oPEsocuaQozAH/dJElsiRkoM8Yec4iHnw0RLkDAXsr
61l7Ik2PIfBNtVHJV/XWu2gfmeRyG3g6D0Zludwgngl+m7bKlCSy6VhhH4hGBBgR
AgAGBQJcOk3TAAoJEObAPln68s0SRGQAn22GXAAIaNrjEwKkGroEaeBj3zyrAJ41
dHUiMLccSoE26YBt0vmVQdfxdQ==
=QQkT
-----END PGP PUBLIC KEY BLOCK-----
```

## Encrypting and Decrypting Files

Let's define the file to encrypt and the output file:

```C#
string rootDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
string inFile = rootDir + "\\safester_samples\\koala.jpg";
string outFile = rootDir + "\\safester_samples\\koala.jpg.pgp";
```
We will prepare an asymmetric / public key encryption using the generated PGP public key, we could use more than one if we wanted to encrypt for several persons, this is why a `List<PgpPublicKey>` is used:

```C#
PgpPublicKey pgpPublicKey = PgpPublicKeyGetter.ReadPublicKey(publicKeyring);
List<PgpPublicKey> encKeys = new List<PgpPublicKey>();
encKeys.Add(pgpPublicKey);
```

We define if we want to Base64 armor the encrypted file and if the file integrity will be checked during decryption and then encrypt the file. 

We will use an `Encryptor`and pass to `Encrypt` method the `List` of `PgpPublicKey` and the two `stream` instances. And because we use a PCL that works on many environments, crypto operations on files are done passing read and write `stream` instances to the library classes (instead of `File` descriptors that are implementation specific).

```c#
// stream is universal, but System.IO.File is Windows only 
// and can not be used in our Safester.CryptoLibrary PCL methods:
Stream inputStream = File.OpenRead(inFile); 
Stream outputStream = File.OpenWrite(outFile);

bool armor = false;
bool withIntegrityCheck = true;

Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
encryptor.Encrypt(encKeys, inputStream, outputStream);
Console.WriteLine("Encryption done.");
```

Note that the `streams` are always safely closed by `Encrypt`.

Decryption is straightforward using a `Decryptor`. We just pass the previous built 

`String privateKeyring = pgpKeyPairHolder.PrivateKeyRing` to the `Decryptor` constructor along with the necessary passphrase:

```C#
string inFileEncrypted = rootDir + "\\safester_samples\\koala.jpg.pgp"; 
string outFileDecrypted = rootDir + "\\safester_samples\\koala_2.jpg";

inputStream = File.OpenRead(inFileEncrypted);
outputStream = File.OpenWrite(outFileDecrypted);

Decryptor decryptor = new Decryptor(privateKeyring, passphrase);
decryptor.Decrypt(inputStream, outputStream);
Console.WriteLine("Decryption integrity check status: " + decryptor.Verify);
Console.WriteLine("Decryption done.");
```
Note that the `streams` are always safely closed by `Decrypt`.

## Encrypting and Decrypting Texts

`Encryptor` and `Decryptor` support string encryption:

```C#
String inText = "For a long time I would go to bed early.";
Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
string outText = encryptor.Encrypt(encKeys, inText);
Console.WriteLine("Encryption done.");
```
The encrypted`outText` is always Base64 armored for future direct usage in decryption:

```pgp
-----BEGIN PGP MESSAGE-----
Version: BCPG C# v1.8.4.0

hI4DG9fyMQsX8noQAf9njW1c3OGbc/IKdCcIcU6WFLy/Y+Qexp458JVhZOZQgs7r
91h+S2j/+fbs8blri2CFLEww+sBylylj5yPzbWh3Af0UPPI5vYyO3mS1FtA1MKIV
IXkjY+SkTg3Lxpwzff+g9OEkkpSLudX5BPfx7oI6jMJhLwHoyXcP4BvNpbLAKTVB
0l0BftMDFyy9h8SkRDc1KTh+R46SgxJC7SWy98tZWPvN8Te1FWcy8i3/9SSjsFr0
YUI62onBZmo8E/M8fDHcSFSzWhgl1INI/gYLlUWE1BXfFsKnrEq/BLUcVZ/Lb1A=
=rni9
-----END PGP MESSAGE-----
```

Decryption is straightforward:

```C#
decryptor = new Decryptor(privateKeyring, passphrase);
string decryptText = decryptor.Decrypt(outText);
Console.WriteLine(decryptText);
```
## Code Samples

More samples are available on GitHub at: https://github.com/kawansoft/Safester.CryptoLibrary 

