
# BiletSatis Projesi

**BiletSatis**, uçuş rezervasyonları ve bilet satışlarını yönetmek için geliştirilmiş bir **ASP.NET Core MVC** uygulamasıdır.

## Projenin Amacı

Bu proje, aşağıdaki işlevleri sağlamak amacıyla geliştirilmiştir:
- Müşteri ve uçuş bilgilerinin yönetimi.
- Rezervasyon ve bilet oluşturma işlemleri.
- Uçuş bazlı bilet filtreleme.

## Özellikler

- **Müşteri Yönetimi:** Müşteri bilgilerini ekleme, düzenleme ve silme.
- **Uçuş Yönetimi:** Uçuş bilgilerini oluşturma ve güncelleme ve listeleme.
- **Bilet Satışı:** Koltuk seçimi ve fiyatlandırma ile bilet oluşturma.
- **Filtreleme:** Uçuş bazlı bilet arama ve listeleme.

## Kullanılan Teknolojiler

- **Backend:** ASP.NET Core MVC, Entity Framework Core
- **Frontend:** Razor Pages, Bootstrap
- **Veritabanı:** SQLite
- **Diğer Araçlar:**
  - Git (Versiyon Kontrol)
  - Visual Studio Code

---

## Kurulum

Bu projeyi yerel makinenize kurmak için aşağıdaki adımları izleyebilirsiniz:

### **1. Projeyi Klonlayın**
Depoyu GitHub’dan klonlayarak başlayın:
```bash
git clone https://github.com/okangulsevil/BiletSatis.git
cd BiletSatis
```

### **2. Bağımlılıkları Yükleyin**
Projeyi çalıştırmadan önce tüm bağımlılıkları yüklemek için şu komutu çalıştırın:
```bash
dotnet restore
```

### **3. Veritabanını Yapılandırın**
- `appsettings.json` dosyasını açın ve `ConnectionStrings` kısmını düzenleyin:
  ```json
  "ConnectionStrings": {
      "DefaultConnection": "Data Source = my.db"
  }
  ```

- Veritabanını güncellemek için:
  ```bash
  dotnet ef database update
  ```

### **4. Uygulamayı Çalıştırın**
Projeyi başlatmak için:
```bash
dotnet run
```

- Tarayıcıda şu adrese gidin:
  ```
  http://localhost:5000
  ```

---


## Katkıda Bulunma

Bu projeye katkıda bulunmak isterseniz aşağıdaki adımları izleyebilirsiniz:

1. **Depoyu Fork Edin:** 
   GitHub hesabınıza depoyu fork edin.

2. **Branş Oluşturun:** 
   Yeni bir özellik için yeni bir branş oluşturun:
   ```bash
   git checkout -b feature/YeniOzellik
   ```

3. **Değişiklikleri Yapın:** 
   İlgili değişiklikleri yerel olarak yapın.

4. **Commit Yapın:**
   Değişikliklerinizi commit edin:
   ```bash
   git commit -m "Yeni özellik eklendi"
   ```

5. **Push Yapın:**
   Değişikliklerinizi uzak depoya gönderin:
   ```bash
   git push origin feature/YeniOzellik
   ```

6. **Pull Request Gönderin:** 
   GitHub üzerinden bir pull request gönderin.

---

## Proje Durumu

Şu anda proje **beta** aşamasındadır. Yeni özellikler ve geliştirmeler için katkıda bulunabilirsiniz.

---

## Lisans

Bu proje [MIT Lisansı](https://opensource.org/licenses/MIT) ile lisanslanmıştır.

---

## İletişim

Projeyle ilgili sorularınız için benimle iletişime geçebilirsiniz:

- **Ad:** Okan Gülsevil
- **E-posta:** [okangulsevil@example.com](mailto:okangulsevil@gmail.com)
- **GitHub:** [okangulsevil](https://github.com/okangulsevil)
