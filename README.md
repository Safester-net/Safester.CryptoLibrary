# Safester.CryptoLibrary 

## Open PGP C# Portable Library - v1.0.5 - January 2019, 14



<img src="https://www.safester.net/img/icon-64x64.png" alt="Safester Icon"/>

# Fundamentals 

Safester.CryptoLibrary is a simple to use portable encryption library based on [OpenPGP](https://www.openpgp.org/). 

It provides :

- PGP key pair generation,
- PGP encryption & decryption for files and texts

For ease of use, the scope of the library is voluntarily limited to encryption operations only. There are no signature methods.

## Technical operating environment – Portable Library

Safester.CryptoLibrary is entirely written in C# and is packaged as a [.NET Standard 1.3](https://docs.microsoft.com/dotnet/standard/net-standard)  portable library, which works on Windows Desktop and on Android, iOS & macOS with [Xamarin](https://visualstudio.microsoft.com/xamarin/).

The underlying crypto library used is [Bouncy Castle](http://www.bouncycastle.org/csharp/) through its [portable implementation](https://www.nuget.org/packages/Portable.BouncyCastle/).

## License

The library is licensed with the liberal [Apache 2.0](https://github.com/kawansoft/Safester.CryptoLibrary/blob/master/LICENSE) license.

## Installation

Install the [NuGet Package](https://www.nuget.org/packages/Safester.CryptoLibrary/).

# Using the Library

## Generating Key Pairs

Key Pair generation is done with the `PgpKeyPairGenerator` class:

```C#
string identity = "john@smith.com";
char [] passphrase = "my_passphrase".ToCharArray();

PgpKeyPairGenerator generator = 
    new PgpKeyPairGenerator(identity, passphrase, PublicKeyAlgorithm.RSA, PublicKeyLength.BITS_2048);
PgpKeyPairHolder pgpKeyPairHolder = generator.Generate();
```

It is then possible to retrieve the Base64 armored PGP keyrings to use for crypto operations:

```C#
String privateKeyring = pgpKeyPairHolder.PrivateKeyRing;
String publicKeyring = pgpKeyPairHolder.PublicKeyRing;
```
This is how the two keyrings look alike when displayed on the console:

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
// Our sample runs on Windows... Just adapt for a Xamarin test.
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

We then define if we want to Base64 armor the encrypted file and if the file integrity will be checked during decryption and then encrypt the file. 

We will use an `Encryptor` and pass to the `Encrypt` method the `List` of `PgpPublicKey`. Because Safester.CrytolLibrary is a portable library that works on many environments, crypto operations on files are done passing read and write `stream` instances to the library classes. (The library thus does not use `File` descriptors that are implementation specific).

```c#
// Our sample runs on Windows. 
// We thus Use System.IO.File to open the in and out streams
Stream inputStream = File.OpenRead(inFile); 
Stream outputStream = File.OpenWrite(outFile);

bool armor = false; // No ASCII Base64 armor, encrypted file will be binary
bool withIntegrityCheck = true;

// Create an Encryptor instance and pass the public keys and streams
Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
encryptor.Encrypt(encKeys, inputStream, outputStream);
Console.WriteLine("Encryption done.");
```

Note that the two `stream` instances  are always safely closed by `Encrypt`.

Decryption is straightforward using a `Decryptor`. We just pass the previous  `privateKeyring`  (built with 

`String privateKeyring = pgpKeyPairHolder.PrivateKeyRing`) to the `Decryptor` constructor along with the necessary passphrase:

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
Note that the two `stream` instances  are always safely closed by  `Decrypt`.

## Encrypting and Decrypting Texts

`Encryptor` and `Decryptor` support string encryption:

```C#
String inText = "For a long time I would go to bed early. Sometimes, " +
    "the candle barely out, my eyes close so quickly that I did not have " +
    "time to tell myself \"I’m falling asleep.\"";
Encryptor encryptor = new Encryptor(armor, withIntegrityCheck);
string outText = encryptor.Encrypt(encKeys, inText);
Console.WriteLine("Encryption done.");
```
The encrypted `outText` is always Base64 armored for future direct usage in decryption:

```pgp
-----BEGIN PGP MESSAGE-----
Version: BCPG C# v1.8.4.0

hI4DaPLU6z90RXIQAf974zEuuTvmisO2Lf3UdbE/SoMfobfKzdygrxxed7i6hTEY
daMJq5Qx0g9hMc8v9C8js4nUkqmXUwTG0UCs6hc+Af90sxxsOSxPol69s++E0Bej
Y+ZwSN5Qb8MS6fvoU3rsFr06eJp8ko80WoNPmbBgFYZovs/dxzJCK18MGSKBY06g
0rgBbgOlD+gdrukCA29U4Bnmrs6Z9qLzKkbaaqTXKCviTFH9zsvXLocA/nzJG8w1
Lv2vb0SohNOSs5RMUkwNB7I0JlgM/7XJ/wPqJktnglxpJsZ7QA/2i6KAjr/MD0Fk
BfSWIfnpQUaUt03nReBa5i5OY4rDGvi7z33t0gZakh3jOss1wNekJSGtuKnINb2+
+eA+EVBtg2X163VrhPNPMMvwssBQyXanRaveZvmQBDZ005w4Tp33fyBc
=UVuj
-----END PGP MESSAGE-----
```

Decryption is straightforward:

```C#
decryptor = new Decryptor(privateKeyring, passphrase);
string decryptedText = decryptor.Decrypt(outText);
Console.WriteLine("Decryption integrity check status: " + decryptor.Verify);
Console.WriteLine(decryptedText);
```
## Code Samples

More samples are available on GitHub at: https://github.com/kawansoft/Safester.CryptoLibrary 

