# Lego Smart Home Specifikáció
(utolsó módosítás: 2017.10.08)
------------------------------
A Lego Smart Home projekt keretében a már meglévő hardveres megoldásainkhoz készítünk egy felhasználóbarát, a Plug&Play elvet támogató, egyszerűen kezelhető felületet, melynek segítségével a felhasználó képes lesz menedzselni az otthonában lévő Lego Smart Home eszközöket otthonról, vagy akár a Föld bármely pontjáról bármilyen internetre csatlakozni képes eszközről (amely támogatja a Chrome böngészőt).
## Webes felület (UI)
A webes felületen keresztül - felhasználói bejelentkezés után - a felhasználó képes kell legyen a Lego Smart Home eszközeit kezelni, családtagjainak hozzáférést nyújtani ezen eszközökhöz, hozzáférésüket korlátozni. 
A webes felületen a következő alkalmazások kell elérhetőek legyenek:
###### Smart Home Designer
Az alkalmazás ergonómikus, felhasználóbarát grafikus felülete segítségével a felhasználó képes kell legyen alap építőelemekből összerakni saját otthonában lévő Smart Home eszközeinek vezérlőprogramját. 
###### Lego Store
Az áruházban mások elkészített vezérlőprogramjait tekinthetjük meg, segítségével könnyebbé, gyorsabbá tehetjük az egyes felhasználók munkáját. Valamint megjelenhetnek benne egyéb, a rendszer használatát segítő segédanyagok (pl hivatalos összeszerelési útmutatók, guide-ok, videók), megvásárolható plusz funkciók, stb.
###### My Reports
Kimutatások segítségével adhatunk visszacsatolást a felhasználónak, melyek segítségével optimalizálhatja Lego Smart Home eszközeinek működését céljainak elérése érdekében. Az alkalmazás színes grafikonokon és diagramokon kívül tippekkel, trükkökkel, ajánlatokkal szolgálhat a felhasználónak.
###### Manage My Homies
Ezen a felületen keresztül menedzselhető a felhasználó Lego Smart Home rendszerét használó többi felhasználó. Itt állítható be, kinek, milyen szintű jogosultsága van használni az egyes eszközöket.
## Webalkalmazás (webapp)
A webalkalmazás feladata a felhasználó és az otthonában lévő Smart Home hardverek közötti kapcsolat létesítése. Nyilvános internetről elérve szolgáltatja a felhasználó felé a webes felületet, az ott végrehajtott módosításokat tárolja, végrehajtja, valamint ezeket az információkat közli a Smart Home rendszerben lévő Raspberry PI eszközzel. 
A kommunikáció egységes programozási interfészen keresztül kell follyon mind a felhasználói felület, mind a Raspberry PI eszközök felé.
## RaspberryPI modul
A Raspberry PI modul az otthoni SmartHome rendszer központi eleme. Rajta futnia kell egy webszervernek, amely ugyanazt a felületet nyújtja, mint a nyilvános weben kint lévő szerver. Ez a weboldal otthonról, internetkapcsolat nélkül is elérhető. 
Továbbá a felhasználó otthonában lévő SmartHome hardvereket menedzseli, belső adatbázisába menti állapotukat, állapotváltozásukat végrehajtatja az eszközökkel.
