namespace Bookshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookshopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Bookshop.Models.BookshopDbContext";
        }

        protected override void Seed(BookshopDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }


            if (!context.Users.Any(u => u.UserName == "Admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "Admin@admin.com",
                    Email = "Admin@admin.com"
                };


                manager.Create(user, "Admin12345");
                manager.AddToRole(user.Id, "Admin");
            }

            context.Authors.AddOrUpdate(x => x.AuthorId,
                new Author() { AuthorId = 1, Name = "George R.R.", Surname = "Martin", Description = "Pisarz, twórca science fiction i fantasy, zarówno opowiadań jak i powieści, wielokrotny zdobywca nagród Nebula i Hugo oraz wielu innych (World Fantasy Award, Bram Stoker Award, Locus Award). Początkowo publikował pod nazwiskiem pozbawionym dwóch \"R\", które dołączył, gdy zaczęto go mylić z managerem The Beatles. Jego utwory przetłumaczono na 14 języków, w tym polski. Jest również autorem popularnej sagi fantasy Pieśń Lodu i Ognia, na której oparto między innymi grę fabularną A Game of Thrones. Na podstawie jego prac nakręcono kilka filmów i odcinków serii telewizyjnych (Remembering Melody, 1984; Nightflyers, 1987; Sandkings, 1995). George R. R. Martin współpracował także przy tworzeniu różnych projektów telewizyjnych (Strefa mroku, Beauty and the Beast, Doorways, itp.)." },
                new Author() { AuthorId = 2, Name = "Andrzej", Surname = "Sapkowski", Description = "Polski pisarz fantasy, z wykształcenia ekonomista. Twórca postaci wiedźmina. Jest najczęściej po Lemie tłumaczonym polskim autorem fantastyki. W 2012 roku minister kultury i dziedzictwa narodowego Bogdan Zdrojewski odznaczył go srebrnym medalem Gloria Artis." },
                new Author() { AuthorId = 3, Name = "J.K.", Surname = "Rowling", Description = "Brytyjska pisarka, autorka serii Harry Potter. Z wykształcenia filolog klasyczny." }
                );

            context.Books.AddOrUpdate(x => x.Id,
                new Book() { Id = 1, Title = "Gra o Tron", PublicationDate = new DateTime(1996, 08, 1), ISBN = "9788365676078", AuthorId = 1, Description = "W Zachodnich Krainach o ośmiu tysiącach lat zapisanej historii widmo wojen i katastrofy nieustannie wisi nad ludźmi. Zbliża się zima, lodowate wichry wieją z północy, gdzie schroniły się wyparte przez ludzi pradawne rasy i starzy bogowie. Zbuntowani władcy na szczęście pokonali szalonego Smoczego Króla, Aerysa Targaryena, zasiadającego na Żelaznym Tronie Zachodnich Krain, lecz obalony władca pozostawił po sobie potomstwo, równie szalone jak on sam... Tron objął Robert - najznamienitszy z buntowników. Minęły już lata pokoju i oto możnowładcy zaczynają grę o tron." },
                new Book() { Id = 2, Title = "Starcie Królów", PublicationDate = new DateTime(1998, 11, 16), ISBN = "9788375069952", AuthorId = 1, Description = "Żelazny Tron jednoczył Zachodnie Królestwa aż do śmierci króla Roberta. Wdowa jednak zdradziła królewskie ideały, bracia wszczęli wojnę, a Sansa została narzeczoną mordercy ojca, który teraz okrzyknął sie królem. Zresztą w każdym z królestw, od Smoczej Wyspy po Koniec Burzy, dawni wasale Żelaznego Tronu ogłaszają się królami. Pewnego dnia z Cytadeli przylatuje biały kruk, przynosząc zapowiedź końca lata, jakie pamiętali żyjący ludzie. Najgroźniejszym wrogiem dla wszystkich bez wyjątku może okazać się nadciągająca zima..." },
                new Book() { Id = 3, Title = "Nawałnica Mieczy. Stal i Śnieg", PublicationDate = new DateTime(2000, 08, 8), ISBN = "9788377852101", AuthorId = 1, Description = "Siedem królestw rozdarła krwawa wojna, a zima zbliża się niczym rozwścieczona bestia. Ludzie z Nocnej Straży przygotowują się na spotkanie z wielkim chłodem i żywymi trupami, które mu towarzyszą. Do inwazji na północ, której świeżo wykutą koronę nosi Robb Stark, szykuje się jednak horda głodnych, dzikich ludzi władających magią nawiedzanego pustkowia. Siostry Robba zaginęły, nie żyją albo w każdej chwili mogą zginąć na rozkaz króla Joffreya z rodu Lannisterów. A za morzem ostatnia z Targaryenów wychowuje smoki, które wykluły się na pogrzebowym stosie jej męża, gotowa pomścić śmierć ojca, ostatniego ze smoczych królów zasiadających na Żelaznym Tronie." },
                new Book() { Id = 4, Title = "Nawałnica Mieczy. Krew i Złoto", PublicationDate = new DateTime(2000, 08, 8), ISBN = "9788377854471", AuthorId = 1, Description = "Siedem królestw rozdarła krwawa wojna, a zima zbliża się niczym rozwścieczona bestia. Ludzie z Nocnej Straży przygotowują się na spotkanie z wielkim chłodem i żywymi trupami, które mu towarzyszą. Do inwazji na północ, której świeżo wykutą koronę nosi Robb Stark, szykuje się jednak horda głodnych, dzikich ludzi władających magią nawiedzanego pustkowia. Siostry Robba zaginęły, nie żyją albo w każdej chwili mogą zginąć na rozkaz króla Joffreya z rodu Lannisterów. A za morzem ostatnia z Targaryenów wychowuje smoki, które wykluły się na pogrzebowym stosie jej męża, gotowa pomścić śmierć ojca, ostatniego ze smoczych królów zasiadających na Żelaznym Tronie." },
                new Book() { Id = 5, Title = "Uczta dla Wron. Cienie Śmierci", PublicationDate = new DateTime(2005, 10, 17), ISBN = "9788377856635", AuthorId = 1, Description = "Po stuleciach ciągłych wojen i zdrad siedem osłabionych potęg dzielących władzę nad krainą zawarło ze sobą rozejm. Tak się przynajmniej wydaje. Po śmierci króla potwora, Joffreya, Cersei przejęła władzę w Królewskiej Przystani. Śmierć Robba Starka złamała kręgosłup buntowi północy, a rodzeństwo Młodego Wilka rozproszyło się po całym królestwie. Trwająca tak długo wojna wreszcie się wypaliła. Jednakże wkrótce zaczynają się zbierać niedobitki, banici, renegaci i padlinożercy, którzy ogryzają kości poległych i łupią tych, którzy wkrótce również rozstaną się z życiem. W Siedmiu Królestwach ludzkie wrony zgromadziły się na bankiet z popiołów…" },
                new Book() { Id = 6, Title = "Uczta dla Wron. Sieć spisków", PublicationDate = new DateTime(2005, 10, 17), ISBN = "9788377856666", AuthorId = 1, Description = "Wojna pięciu królów zbliża się do końca. Lannisterowie i ich sojusznicy uważają się za zwycięzców, jednakże nie wszystko w kraju idzie dobrze. Niezadowoleni z ich rządów wciąż spiskują i zawierają nowe sojusze. Martellowie z Dorne szukają zemsty za swych zabitych krewniaków, a dziedzic króla Balona z Żelaznych Wysp, Euron Wronie Oko, jest najstraszliwszym piratem, jaki kiedykolwiek pływał po morzach. Brienne Ślicznotka, szuka Sansy Stark, której poprzysięgła bronić przed gniewem królowej Cersei. To czas, w którym szlachetnie urodzeni i prości ludzie, żołnierze i czarodzieje, skrytobójcy i mędrcy muszą połączyć siły, ponieważ na uczcie dla wron jest wielu gości, ale tylko nieliczni ujdą z niej z życiem." },
                new Book() { Id = 7, Title = "Taniec ze smokami. Część I", PublicationDate = new DateTime(2011, 07, 12), ISBN = "9788377858851", AuthorId = 1, Description = "Na wschodzie Daenerys Targaryen, ostatnia z rodu Targaryenów, włada przy pomocy swych trzech smoków miastem zbudowanym na marzeniach i pyle. Daenerys ma jednak tysiące wrogów i wielu z nich postanowiło ją odnaleźć... \n\n Tyrion Lannister, uciekłszy z Westeros, gdy wyznaczono nagrodę za jego głowę, również zmierza do Daenerys. Jego nowi towarzysze podróży nie są jednak obdartą bandą wyrzutków, jaką mogliby się wydawać, a jeden z nich może na zawsze pozbawić Daenerys praw do westeroskiego tronu... \n\n Północy broni gigantyczny Mur z lodu i kamienia. Tam właśnie przed Jonem Snow, dziewięćset dziewięćdziesiątym ósmym lordem dowódcą Nocnej Straży, stanie najpoważniejsze jak dotąd wyzwanie. Ma bowiem potężnych wrogów nie tylko w samej Straży, lecz również poza nią, pośród lodowych istot mieszkających za Murem... \n\n Wszędzie narastają gwałtowne konflikty, w których uczestniczą banici i kapłani, żołnierze i zmiennokształtni, szlachetnie urodzeni i niewolnicy. W czasie narastających niepokojów fale przeznaczenia nieuchronnie prowadzą bohaterów do największego ze wszystkich tańców..." },
                new Book() { Id = 8, Title = "Taniec ze smokami. Część II", PublicationDate = new DateTime(2011, 07, 12), ISBN = "9788377858875", AuthorId = 1, Description = "W Meereen otoczona przez wrogów Daenerys Targaryen chwyta się rozpaczliwych środków, by zachować swoją pozycję. Wielu zabiega o jej względy, lecz ostrzegano ją, by nie ufała nikomu... \n\n W Królewskiej Przystani królowa regentka, Cersei Lannister, oczekuje na proces, porzucona przez tych, którym ufała, podczas gdy w położonym na wschodzie mieście Yunkai jej brat Tyrion został sprzedany w niewolę i jest zmuszony na własną rękę szukać wyjścia z opresji. \n\n W Braavos Arya Stark poznaje tajemnice Ludzi Bez Twarzy i pamiętanie o swym dziedzictwie sprawia jej coraz większe trudności. \n\n Stannis Baratheon pozostawił żonę oraz czerwoną kapłankę Melisandre i pomaszerował na południe, by stawić czoło okupującym Winterfell Boltonom. Tymczasem za Murem armie Dzikich przygotowują się do szturmu... \n\n Wszędzie narastają gwałtowne konflikty, w których uczestniczą banici i kapłani, żołnierze i zmiennokształtni, szlachetnie urodzeni i niewolnicy. W czasie narastających niepokojów fale przeznaczenia nieuchronnie prowadzą bohaterów do największego ze wszystkich tańców..." },

                new Book() { Id = 9, Title = "Sezon Burz", PublicationDate = new DateTime(2013, 05, 20), ISBN = "978-83-7578-059-8", AuthorId = 2, Description = "Oto nowy Sapkowski i nowy wiedźmin. Mistrz polskiej fantastyki znowu zaskakuje. „Sezon burz” nie opowiada bowiem o młodzieńczych latach białowłosego zabójcy potworów ani o jego losach po śmierci/nieśmierci kończącej ostatni tom sagi. \"Nigdy nie mów nigdy!\" W powieści pojawiają się osoby doskonale czytelnikom znane, jak wierny druh Geralta – bard i poeta Jaskier – oraz jego ukochana, zwodnicza czarodziejka Yennefer, ale na scenę wkraczają też dosłownie i w przenośni postaci z zupełnie innych bajek. Ludzie, nieludzie i magiczną sztuką wyhodowane bestie. Opowieść zaczyna się wedle reguł gatunku: od trzęsienia ziemi, a potem napięcie rośnie. Wiedźmin stacza morderczą walkę z drapieżnikiem, który żyje tylko po to, żeby zabijać, wdaje się w bójkę z rosłymi, niezbyt sympatycznymi strażniczkami miejskimi, staje przed sądem, traci swe słynne miecze i przeżywa burzliwy romans z rudowłosą pięknością, zwaną Koral. A w tle toczą się królewskie i czarodziejskie intrygi. Pobrzmiewają pioruny i szaleją burze. I tak przez 404 strony porywającej lektury." },
                new Book() { Id = 10, Title = "Ostatnie Życzenie", PublicationDate = new DateTime(1993, 01, 10), ISBN = "83-7054-061-9", AuthorId = 2, Description = "Później mówiono, że człowiek ów nadszedł od północy, od Bramy Powroźniczej. Nie był stary, ale włosy miał zupełnie białe. Kiedy ściągnął płaszcz, okazało się, że na pasie za plecami ma miecz. \n\n Białowłosego przywiodło do miasta królewskie orędzie: trzy tysiące orenów nagrody za odczarowanie nękającej mieszkańców Wyzimy strzygi. \n\n Takie czasy nastały. Dawniej po lasach jeno wilki wyły, teraz namnożyło się rozmaitego paskudztwa – gdzie spojrzysz, tam upiory, bazyliszki, diaboły, żywiołaki, wiły i utopce plugawe. A i niebacznie uwolniony z amfory dżinn, potrafiący zamienić życie spokojnego miasta w koszmar, się trafi. \n\n Tu nie wystarczą zwykłe czary ani osinowe kołki. Tu trzeba zawodowca. \n\n WIEDŹMINA. \n\n Mistrza magii i miecza. Tajemną sztuką wyuczonego, by strzec na świecie moralnej i biologicznej równowagi. " },
                new Book() { Id = 11, Title = "Miecz Przeznaczenia", PublicationDate = new DateTime(1992, 07, 8), ISBN = "83-7054-037-6", AuthorId = 2, Description = "" },
                new Book() { Id = 12, Title = "Krew Elfów", PublicationDate = new DateTime(1994, 12, 16), ISBN = "83-7054-079-1", AuthorId = 2, Description = "" },
                new Book() { Id = 13, Title = "Czas Pogardy", PublicationDate = new DateTime(1995, 08, 8), ISBN = "83-7054-091-0", AuthorId = 2, Description = "" },
                new Book() { Id = 14, Title = "Chrzest Ognia", PublicationDate = new DateTime(1996, 10, 17), ISBN = "83-7054-103-8", AuthorId = 2, Description = "" },
                new Book() { Id = 15, Title = "Wieża Jaskółki", PublicationDate = new DateTime(1997, 02, 28), ISBN = "83-7054-124-0", AuthorId = 2, Description = "" },
                new Book() { Id = 16, Title = "Pani Jeziora", PublicationDate = new DateTime(2011, 03, 03), ISBN = "978-83-7578-034-5", AuthorId = 2, Description = "" },

                new Book() { Id = 17, Title = "Harry Potter i Kamień Filozoficzny", PublicationDate = new DateTime(1997, 04, 10), ISBN = "978-83-7278-000-3", AuthorId = 3, Description = "" },
                new Book() { Id = 18, Title = "Harry Potter i Komnata Tajemnic", PublicationDate = new DateTime(1998, 07, 2), ISBN = "978-83-7278-007-2", AuthorId = 3, Description = "" },
                new Book() { Id = 19, Title = "Harry Potter i więzień Azkabanu", PublicationDate = new DateTime(1999, 07, 8), ISBN = "9788380083738", AuthorId = 3, Description = "" },
                new Book() { Id = 20, Title = "Harry Potter i Czara Ognia", PublicationDate = new DateTime(2000, 07, 8), ISBN = "9788380082175", AuthorId = 3, Description = "" },
                new Book() { Id = 21, Title = "Harry Potter i Zakon Feniksa", PublicationDate = new DateTime(2003, 06, 21), ISBN = "83-7278-096-6", AuthorId = 3, Description = "" },
                new Book() { Id = 22, Title = "Harry Potter i Książę Półkrwi", PublicationDate = new DateTime(2005, 07, 16), ISBN = "9788380082212", AuthorId = 3, Description = "" },
                new Book() { Id = 23, Title = "Harry Potter i Insygnia Śmierci", PublicationDate = new DateTime(2007, 07, 21), ISBN = "978-83-7278-280-9", AuthorId = 3, Description = "" }
                );
        }
    }
}
