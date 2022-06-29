--create database asas

CREATE TABLE "Product"(
    "ProductsId" INT identity(1,1) NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Price" INT NOT NULL,
    "DiscriptionProducts" NVARCHAR(max) NULL,
    "ImageProducts" NVARCHAR(255) NULL,
	"Count" int not null 
);
ALTER TABLE
    "Product" ADD CONSTRAINT "product_productsid_primary" PRIMARY KEY("ProductsId");
CREATE TABLE "Service"(
    "ServiceId" INT identity(1,1) NOT NULL,
    "Name" NVARCHAR(255) NOT NULL,
    "Price" INT NOT NULL,
    "DurationService" INT NOT NULL,
    "DiscriptionService" NVARCHAR(max) NULL,
    "ImageService" NVARCHAR(255) NULL
);
ALTER TABLE
    "Service" ADD CONSTRAINT "service_serviceid_primary" PRIMARY KEY("ServiceId");
CREATE TABLE "Client"(
    "ClientId" INT identity(1,1) NOT NULL,
    "FirstName" NVARCHAR(255) NOT NULL,
    "MiddleName" NVARCHAR(255) NOT NULL,
    "LastName" NVARCHAR(255) NULL,
    "PhoneNumber" NVARCHAR(255) NULL,
    "Email" NVARCHAR(255) NULL
);
ALTER TABLE
    "Client" ADD CONSTRAINT "client_clientid_primary" PRIMARY KEY("ClientId");
CREATE TABLE "ClientSevice"(
    "ClientSeviceId" INT identity(1,1) NOT NULL,
    "ClientId" INT NOT NULL,
    "ServiceId" INT NOT NULL,
    "DataTimeService" DATETIME NOT NULL
);
ALTER TABLE
    "ClientSevice" ADD CONSTRAINT "clientsevice_clientseviceid_primary" PRIMARY KEY("ClientSeviceId");
CREATE TABLE "ClientProduct"(
    "ClientProductId" INT identity(1,1) NOT NULL,
    "ClientId" INT NOT NULL,
    "ProductId" INT NOT NULL,
    "Amount" INT NOT NULL
);
ALTER TABLE
    "ClientProduct" ADD CONSTRAINT "clientproduct_clientproductid_primary" PRIMARY KEY("ClientProductId");
ALTER TABLE
    "ClientProduct" ADD CONSTRAINT "clientproduct_productid_foreign" FOREIGN KEY("ProductId") REFERENCES "Product"("ProductsId");
ALTER TABLE
    "ClientSevice" ADD CONSTRAINT "clientsevice_serviceid_foreign" FOREIGN KEY("ServiceId") REFERENCES "Service"("ServiceId");
ALTER TABLE
    "ClientSevice" ADD CONSTRAINT "clientsevice_clientid_foreign" FOREIGN KEY("ClientId") REFERENCES "Client"("ClientId");
ALTER TABLE
    "ClientProduct" ADD CONSTRAINT "clientproduct_clientid_foreign" FOREIGN KEY("ClientId") REFERENCES "Client"("ClientId");

Insert Into dbo.Product (Name, Price, DiscriptionProducts, ImageProducts,Count)
Values 
('Шампунь профессиональный для укрепления волос', 1200, 'Эффективная формула с коллагеном увлажняет питает и восстанавливает структуру волос защищает волосы от внешних воздействий. Комбинация коллагена и протеинов пшеницы обеспечат волосам прочность, упругость, эластичность и блеск, создавая невидимую защитную пленку', 'imageProduct\шампунь1.jpg',100 ),
('Кондиционер профессиональный для увлажнения волос', 800, 'Маска бальзам с коллагеном и протеинами шелка. Идеально подходит для сухих, ломких и ослабленных волос. Интенсивное средство для глубокого восстановления поврежденных, ломких и слабых волос', 'imageProduct\кондиционер.jpg',100),
('Сыворотка для лица', 2000, 'Сыворотка оказывает мгновенный эффект омоложения кожи. Выравнивает её рельеф, борется с несовершенствами. Подходит для проблемной кожи и кожи с постакне.', 'imageProduct\сыворотка_для_лица.jpg' ,100),
('Тени для век', 1700, 'Идеально подобранная цветовая палитра оттенков включает в себя 18 разных цветов. От нежных и матовых до ярких металликов, цветовая палитра позволит вам экспериментировать и воплощать в реальность любые ваши фантазии с макияжем для лица.', 'imageProduct\тени_для_век.jpg',100),
('Тени для век esense', 1700, 'Идеально подобранная цветовая палитра оттенков включает в себя 18 разных цветов. От нежных и матовых до ярких металликов, цветовая палитра позволит вам экспериментировать и воплощать в реальность любые ваши фантазии с макияжем для лица.', 'imageProduct\тени_для_век_essence.jpg',100),
('Помада для губ', 999, 'Всеми любимая суперстойкая жидкая матовая помада Super Stay Matte Ink. Яркие и модные цвета помогут выразить себя так, как хочешь ты! Удобный аппликатор обеспечивает самое точное и простое нанесение, питает коллагеном','imageProduct\помада.jpп',100),
('Мультифункциональный спрей сывортка для волос', 3500, 'Мультифункциональная спрей сыворотка увлажняет, питает и наполняет энергией тусклые волосы','imageProduct\сыворотка_для_волос.jpg',100),
('Кондиционер для поврежденных волос', 1200, 'Кондиционер Visible Repair мгновенно восстанавливает и питает поврежденные волосы. Укрепляет волосы и облегчает расчесывание от корней до кончиков, придает невероятную мягкость и блеск', 'imageProduct\кондиционер2.jpg',100),
('Тушь для ресниц', 1500, 'Упругая пластиковая щеточка с зауженным кончиком позволяет равномерно распределить тушь по ресницам, разделяя их и придавая суперобъем', 'imageProduct\тушь.jpg',100 ),
('Увлажняющий спрей для волос', 500, 'Легкий крем-спрей обладает потрясающими свойствами: он великолепно увлажняет и питает волосы, облегчает расчесывание, восстанавливает повреждения, обладает термозащитой и защищает цвет окрашенных волос, усиливая их яркость.','imageProduct\спрей.jpg',100)

Insert Into Service (Name,Price,DurationService,DiscriptionService,ImageService)
Values
('Наращивание ресниц',1400, 140, 'Наращивание ресниц - это  процедура добавление к собственным ресницам искусственных', 'imageService\наращивание_ресниц.jpg'),
('Перманентный макияж бровей',6000, 360, 'Перманентный макияж бровей – процедура, во время которой под кожу вводится пигмент с целью коррекции формы, густоты и цвета бровей. Это несмываемый мейкап, выполненный методом поверхностной татуировки, поэтому перманентный макияж ещё называют татуажем.', 'imageService\перманентный_макияж_губ.jpg'),
('Перманентный макияж губ',6000, 360, 'Перманентный макияж губ — микропигментирование кожи — представляет собой введение в верхние слои специальных красящих веществ — пигментов. Большое преимущество этой процедуры в том, необходимый оттенок сохраняется надолго.', 'imageService\перманентный_макияж.jpg'),
('Наращивание ногтей',1700, 120, 'Наращивани моделирование ногтей — процесс искусственного увеличения длины ногтя с целью исправления дефектов натурального ногтя', 'imageService\перманентный_макияж_губ.jpg'),
('Комбинированный маникюр',1100, 120, 'Комбинированный маникюр – это маникюр, который выполняется сразу несколькими техниками: обрезной, европейской и с помощью аппарата', 'imageService\наращивание_ногтей.jpg'),
('Плетение кос',4000, 240, 'Плетение кос с добавлением искственных волос разных цветов в ваши волосы', 'imageService\маникюр.jpg'),
('Вечернаяя прическа',2100, 120, 'Вечерние приечски - легко, просто и красиво. Любая прическа на Ваш вкус, которая станет отличным дополнением к вашему образу', 'imageService\плетение_кос.jpg'),
('Вечерныий макияж',1500, 100, 'Вечерний макияж - ярко, легко и красиво. Любой макияж на Ваш вкус, который станет отличным дополнением к вашему образу', 'imageService\вечерняя_прическа.jpg'),
('Лифтинг-уход',2500, 120, 'Рекомендуется для восстановления тургора кожи лица, шеи и области декольте. Основные эффекты: устраняет дряблость и вялость, тонизирует, структурирует кожу; увлажняет и регенерирует.', 'imageService\лифтинг.jpg'),
('Оформление бровей',700, 30, 'Приведём Ваши брови в порядок быстро, качество. Процедура выполняеьтся ниточной техникой и пинцетом', 'imageService\оформление-бровей.jpg'),
('Укладка',2000, 90, 'Придадим форму и объем Вашим волосам с помощью фена и горячих электроинструментов.', 'imageService\укладка.jpg'),
('Пирсинг',1000, 20, 'Пи́рсинг — одна из форм модификаций тела, создание прокола, в котором носят украшения. Создадим прокол, где желаете и подберем самое красивое украшение', 'imageService\пирсинг.jpg'),
('SPA-день',3000, 240, 'Массаж гбулоких тканей+массаж горячими камнями+питающая для лица маска', 'imageService\спа.jpg') 

Insert Into Client (FirstName,MiddleName,LastName,PhoneNumber,Email) Values
('Зайцева', 'Мария', 'Тимофеевна', '8-999-722-33-44', 'maria11@mail.ru'),
('Сергеева', 'Ульяна', 'Арсентьевна', '8-927-666-55-24', 'sergeevaq1@mail.ru'),
('Лукьянова', ' София ', 'Мирославовна', '8-999-724-69-4', 'sogyt@mail.ru'),
('Дубровина', 'Айлин', 'Владимировна', '8-937-444-58-98', 'aiaia@mail.ru'),
('Щербакова', 'Ева ', 'Артёмовна', '8-999-725-77-88', 'evadeva@mail.ru'),
('Зайцева', 'Мария', 'Тимофеевна', '8-999-724-87-99', 'zaucmari@mail.ru'),
('Кудряшова', 'Мария', 'Сергеевна', '8-999-724-58-47', 'kydri@mail.ru'),
('Чернова', 'Мария', 'Тимофеевна', '8-964-458-24-69', 'merichet@mail.ru'),
('Смирнова', 'Вероника', 'Денисовна', '8-988-455-33-44', 'mirsnami@mail.ru'),
('Киселева ', 'Эмилия', 'Владимировна', '8-927-744-25-44', 'emiem9@mail.ru'),
('Бурова ', 'Таисия', 'Глебовна', '8-964-355-78-44', 'taisiis@mail.ru'),
('Львова', 'Кира', 'Павловна', '8-444-89-77', 'kiralira@mail.ru')
