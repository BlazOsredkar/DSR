# Vaja 7 — Web API (nakupi, kosarica, zgodovina)

Cilj je dodati **model nakupa**, **Web API kontroler** in **kosarico**, ki pri zakljucku klice API.

## 1) Modeli in DTOji

- Nakup: [1_test/Models/Purchase.cs](1_test/Models/Purchase.cs)
- Postavka nakupa: [1_test/Models/PurchaseItem.cs](1_test/Models/PurchaseItem.cs)
- DTOji za API: [1_test/Models/Dto/PurchaseDtos.cs](1_test/Models/Dto/PurchaseDtos.cs)

Nakup vsebuje:

- datum, status
- naslov dostave, kontaktni telefon
- seznam postavk (izdelek + kolicina)

## 2) Web API kontroler

Kontroler: [1_test/Controllers/PurchasesApiController.cs](1_test/Controllers/PurchasesApiController.cs)

Podprti endpointi:

- `GET /api/purchases/my` — vrne nakupe trenutnega uporabnika
- `GET /api/purchases` — vrne vse nakupe (samo admin)
- `POST /api/purchases` — ustvari nov nakup (uporabi podatke iz kosarice)

Primer DTO-ja za POST:

```json
{
  "deliveryAddress": "Testna ulica 1",
  "contactPhone": "041000000",
  "note": "Prosim za hitro dostavo",
  "items": [
    { "bookId": 1, "quantity": 2 },
    { "bookId": 2, "quantity": 1 }
  ]
}
```

## 3) Kosarica in checkout

MVC kontroler: [1_test/Controllers/PurchasesController.cs](1_test/Controllers/PurchasesController.cs)

Proces:

1. Uporabnik na strani knjig klikne **"Dodaj v kosarico"**.
2. Kosarica se hrani v **Session** (glej `SessionExtensions`).
3. Na **Blagajni** vpisemo naslov in telefon.
4. Ob oddaji se poklice Web API (`POST /api/purchases`).
5. Ob uspehu se kosarica pocisti in prikaze zgodovina nakupov.

Pogledi:

- Kosarica: [1_test/Views/Purchases/Cart.cshtml](1_test/Views/Purchases/Cart.cshtml)
- Blagajna: [1_test/Views/Purchases/Checkout.cshtml](1_test/Views/Purchases/Checkout.cshtml)
- Zgodovina: [1_test/Views/Purchases/History.cshtml](1_test/Views/Purchases/History.cshtml)

## 4) Pregled nakupov

- Uporabnik vidi **svoje** nakupe.
- Admin vidi **vse** nakupe preko `/Purchases/All`.

## 5) Kako preizkusiti

1. Prijavi se kot `user@dsr.local` (geslo `User123!`).
2. Dodaj knjigo v kosarico.
3. Pojdi na **Blagajno** in zakljuci nakup.
4. Preveri **Moji nakupi**.

> API lahko preveris tudi z orodji, kot so Postman ali curl.
