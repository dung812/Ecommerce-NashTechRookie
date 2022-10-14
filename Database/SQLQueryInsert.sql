USE ShoesShopAssignment
GO

-- INSERT DATA FOR ShoesShopAssignment DATABASE
INSERT [dbo].[Roles] ([RoleName], [Note]) VALUES 
(N'Manager', NULL),
(N'Employee', NULL)

INSERT [dbo].[Admins] ([UserName], [Password], [FirstName], [LastName], [Email], [Phone], [Birthday], [Gender], [RegisteredDate], [Avatar], [Status], [RoleId]) VALUES 
(N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'Nguyen', N'Dung', 'ntdung8124@gmail.com', N'03123123', CAST(N'2022-07-26' AS Date), 1, CAST(N'2022-07-26T15:52:19.833' AS DateTime), N'avatar.jpg', 1, 1)

INSERT [dbo].[Attributes] ([Name]) VALUES 
(N'Size'),
(N'Color')

INSERT [dbo].[AttributeValues] ([AttributeId], [Name]) VALUES 
(1, N'39'), 
(1, N'40'),
(1, N'41'),
(1, N'42'),
(2, N'White'),
(2, N'Black')

INSERT [dbo].[Catalogs] ([Name], [Status]) VALUES 
(N'Boots', 1),
(N'Clogs and Mules', 1),
(N'Flats', 1),
(N'Heels', 1),
(N'Sandals', 1),
(N'Sneakers', 1)

INSERT [dbo].[Manufactures] ([Name], [Logo], [Status]) VALUES 
(N'Nike', N'nike.jpg', 1),
(N'Converse', N'converse.jpg', 1),
(N'Vans', N'vans.jpg', 1),
(N'Crocs', N'crocs.jpg', 1),
(N'LifeStride', N'llifeStride.jpg', 1),
(N'Birkenstock', N'birkenstock.jpg', 1)


INSERT INTO [dbo].[Payments] (PaymentName, [Status]) VALUES
(N'Cash on delivery', 1),
(N'Paypal', 1)

INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Chuck Taylor All Star Street Sneaker Boot', N'93702_pair_large.jpg', N'93702_pair_large', 69, 10, N'Leather or canvas upper in a casual', 50, 1, 1, CAST(N'2022-07-26T16:11:20.833' AS DateTime), 1, 2, 6)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''''s Arizona Footbed Sandal', N'51640_pair_large.jpg', N'51640_pair_large', 109, 0, N'Birkenstock is committed to environmentally friendly operations.', 50, 1, 0, CAST(N'2022-07-26T16:13:25.363' AS DateTime), 1, 6, 5)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Classic Clog', N'70370_pair_large.jpg', N'70370_pair_large', 49, 5, N'Fully-molded Croslite material upper in a casual clog style with a round toe', 50, 1, 0, CAST(N'2022-07-26T15:57:13.037' AS DateTime), 1, 4, 2)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Combs Lace Up Boot', N'51164_pair_large.jpg', N'51164_pair_large', 100, 0, N'Light poly and Ajax leather upper in a lace-up boot style with a round toe', 50, 1, 0, CAST(N'2022-07-26T16:22:04.143' AS DateTime), 1, 6, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Combs Tech Combat Boot', N'52388_pair_large.jpg', N'52388_pair_large', 150, 5, N'Leather upper in a combat boot style with a round toe', 50, 1,1, CAST(N'2022-07-26T16:24:46.740' AS DateTime), 1, 6, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Ward Low Top Sneaker', N'58863_pair_large.jpg', N'58863_pair_large', 56, 10, N'Suede or canvas upper in a low top skate sneaker style with a round toe', 50, 1,1, CAST(N'2022-07-26T16:28:43.920' AS DateTime), 1, 3, 6)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Court Vision Low Sneaker', N'50374_pair_large.jpg', N'50374_pair_large', 75, 0, N'For some styles the leather upper has been replaced with recycled and synthetic materials that keep the soul of the original style', 50, 1,1, CAST(N'2022-07-26T16:33:17.433' AS DateTime), 1, 1, 6)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Tango 2 Medium/Wide Wedge Sandal', N'57753_pair_large.jpg', N'57753_pair_large', 40, 5, N'Slip into a classic wedge that goes well with anything, the Women''s LifeStride Tango 2 Medium/Wide Wedge Sandal.', 50, 1, 0, CAST(N'2022-07-26T16:39:09.707' AS DateTime), 1, 5, 4)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Ward Pride Low Top Sneaker', N'20220731001046344.jpg', N'20220731001046344', 69, 5, N'<p>In celebration of Pride, Vans is donating a total of $200,000 from the Checkerboard Fund at Tides Foundation to three organizations committed to advocating for and providing the space, access and community needed to uplift the voices of the LBGTQ+ community.</p>', 80, 1, 0, CAST(N'2022-07-31T00:10:46.817' AS DateTime), 1, 3, 6)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Classic Platform Clog', N'20220920111831180.jpg', N'20220920111831180', 59, 0, N'<p>Your favorite style gets a lift with the Women''s Crocs Classic Platform Clog.</p>
<ul>
<li>Fully-molded Croslite material upper in a platform clog style with a round toe</li>
<li>Perforated upper and backstrap design for your Jibbitz&trade; charms</li>
<li>Odor-resistant, easy to clean, and quick to dry</li>
<li>Iconic lightweight construction</li>
<li>Croslite&trade; foam cushioning offers all-day comfort and support</li>
<li>Flexible, non-marking outsole</li>
<li>1.6" platform height</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T11:18:32.217' AS DateTime), 1, 4, 2)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Air Max Torch 4 Running Shoe', N'20220920112428409.jpg', N'20220920112428409', 20, 0, N'<p>A running shoe doesn&rsquo;t get any more sleek and sophisticated than the Nike Torch.<br /><br /></p>
<ul>
<li>Mesh and synthetic upper in an athletic running style</li>
<li>Lace-up front, padded tongue and collar</li>
<li>Nike Air technology provides shock absorption</li>
<li>Stylish overlays add support</li>
<li>Contrasting logo overlay</li>
<li>Mesh lining, cushioning footbed</li>
<li>Carbon rubber outole</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T11:24:28.677' AS DateTime), 1, 1, 6)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Cleo 2.0 Flat', N'20220920114630476.jpg', N'20220920114630476', 70, 8, N'<p>Good for your feet and good for the environment: the Women''s Skechers Cleo 2.0 Flat.</p>
<ul>
<li>Engineered knit upper in a skimmer flat style with an almond toe</li>
<li>Slip-on entry</li>
<li>Upper knitted fabric made with 75% recycled polyester</li>
<li>Crafted with 100% vegan materials</li>
<li>Soft lining with Air-Cooled Memory Foam&reg; cushioned comfort insole</li>
<li>Stretch Fit&reg; slip-on design for sock-like comfort</li>
<li>Flexible rubber traction outsole</li>
<li>Machine washable</li>
<li>158343</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T11:46:30.767' AS DateTime), 1, 5, 3)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Batavia Wide Flat', N'20220920115155922.jpg', N'20220920115155922', 85, 20, N'<p>Show off an enchanting look with the Women''s Journee Collection Batavia Wide Flat.</p>
<ul>
<li>Mesh and man-made upper in a flat shoe style with a pointed toe</li>
<li>Slip on entry</li>
<li>Rhinestone accents</li>
<li>Padded footbed</li>
<li>Scalloped heel</li>
<li>Durable rubber outsole</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T11:51:56.177' AS DateTime), 1, 5, 3)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Aphrodite Tall Boot', N'20220920115747095.jpg', N'20220920115747095', 129, 35, N'<p>Step out in style with the Women''s Baretraps Aphrodite Tall Boot.</p>
<ul>
<li>Faux leather upper in a tall boot style with a round toe</li>
<li>Side zipper entry</li>
<li>Metal buckle strap detail</li>
<li>Smooth lining with a padded insole</li>
<li>Durable rubber lugged outsole</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T11:57:47.427' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Tillston 6 Inch Bootie', N'20220920120335397.jpg', N'20220920120335397', 139, 14, N'<p>Take steady strides in the feminine charm of the Women''s Timberland Tillston 6 Inch Bootie.</p>
<ul>
<li>Leather upper in a bootie style with a round toe</li>
<li>Premium leather uppers from a Silver-rated tannery</li>
<li>Lace up closure</li>
<li>Defender Repellent Systems&reg; stain-blocking treatment</li>
<li>Padded collar</li>
<li>Smooth lining with EVA footbed</li>
<li>Durable traction outsole</li>
<li>Textured rubber heel and outsole are made with 15% recycled rubber</li>
<li>11.5" boot circumference, 3" heel height</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T12:03:35.887' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Eurosprint Mid Hiker Boot', N'20220920121206089.jpg', N'20220920121206089', 129, 0, N'<p>Get out and experience nature in comfort and style with the Men''s Timberland Eurosprint Mid Hiker Boot.</p>
<ul>
<li>Nubuck upper in a hiking boot style with a round toe</li>
<li>Lace up front</li>
<li>Reflective Timberland branding decal</li>
<li>Padded collar and tongue for added ankle support</li>
<li>Breathable lining with a cushioned insole</li>
<li>EVA cushioned midsole</li>
<li>Lugged rubber traction outsole</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:12:06.350' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Redwood Falls Chelsea Boot', N'20220920121423410.jpg', N'20220920121423410', 119, 0, N'<p>Keep your feet comfortable all day long in the Men''s Timberland Redwood Falls Chelsea Boot.</p>
<ul>
<li>Leather upper in a chelsea boot style with a round toe</li>
<li>Slip on entry</li>
<li>Dual elastic goring for easy slip-on entry</li>
<li>Heel pull tab</li>
<li>Smooth lining with a padded insole</li>
<li>Timberland branding side decal</li>
<li>Durable rubber lugged outsole</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:14:23.827' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Grayton Lace-Up Boot', N'20220920122307777.jpg', N'20220920122307777', 129, 38, N'<p>A lace-up combat boot with standout style and comfort you&rsquo;ll love.</p>
<ul>
<li>SUSTAINABLY CRAFTED: Linings and topcloth made from recycled plastic bottles, recycled toe box and heel counter</li>
<li>MATERIALS: Faux leather upper</li>
<li>FIT: Lace-up front with back pull tab for easy on/off and padded collar for extra cushioning</li>
<li>SUSTAINABLE COMFORT: Anti-microbial, anti-odor Susterra&reg; foam insole crafted from 11% bio-based materials with soft and lightweight cushioning, built-in arch support, and a molded heel cup</li>
<li>EASY LANDING COMFORT SYSTEM: Ultra-flexible and pliable design with a rounded heel, anatomical heel cup and arch for underfoot comfort, no-slip heel padding, UltimateFlex built-in flex grooves, and soft and light outsole</li>
<li>STYLE: Combat-inspired lace-up boot with a lug sole</li>
<li>CONSCIOUSLY PACKAGED: This style is shipped in a box made with recyclable materials &amp; soy-based inks</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:23:08.343' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Combs Tech Combat Boot', N'20220920123542013.jpg', N'20220920123542013', 109, 0, N'<p>The Men''s Dr. Martens Combs Tech Combat Boot is an extra tough boot with exceptional strength and elasticity.</p>
<ul>
<li>Leather upper in a combat boot style with a round toe</li>
<li>Lace up front</li>
<li>Hook and loop strap closure</li>
<li>Heel pull tab</li>
<li>Padded collar and tongue</li>
<li>Smooth lining with a padded insole</li>
<li>Durable rubber outsole</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:35:42.563' AS DateTime), 1, 5, 1)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Classic Clog', N'20220920124208520.jpg', N'20220920124208520', 54, 0, N'<p>You can''t go wrong with the comfort of the Classic Clog from Crocs.</p>
<ul>
<ul>
<li>Fully-molded Croslite&trade; material upper in a casual clog style with a round toe</li>
</ul>
</ul>
<ul>
<ul>
<li>Holes for breathability</li>
</ul>
</ul>
<ul>
<ul>
<li>Advanced toe box ventilation</li>
</ul>
</ul>
<ul>
<ul>
<li>Odor-resistant, easy to clean, and quick to dry</li>
</ul>
</ul>
<ul>
<ul>
<li>Water-friendly and buoyant</li>
</ul>
</ul>
<ul>
<ul>
<li>Radically lightweight</li>
</ul>
</ul>
<ul>
<ul>
<li>Customizable with Jibbitz&trade; charms</li>
</ul>
</ul>
<ul>
<ul>
<li>Croslite&trade; material heel strap for secure fit</li>
</ul>
</ul>
<ul>
<ul>
<li>Smooth lining, contoured orthotic footbed</li>
</ul>
</ul>
<ul>
<ul>
<li>Lightweight, non-marking sole</li>
</ul>
</ul>
<ul>
<li>In between sizes? Order the next size up!</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:42:08.787' AS DateTime), 1, 4, 2)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Men''s Boston Soft Footbed Clog', N'20220920124543720.jpg', N'20220920124543720', 154, 0, N'<p>A classic to wear all year long: the Men''s Birkenstock Boston Soft Footbed Clog.</p>
<ul>
<li>Suede or leather upper in a comfort clog style with a round toe</li>
<li>Slip-on entry</li>
<li>Adjustable buckle accent</li>
<li>Original BIRKENSTOCK soft, anatomically-shaped footbed</li>
<li>Durable EVA outsole</li>
<li>Birkenstock is committed to environmentally friendly operations. A high proportion of the natural materials that we use are from sustainable sources: cork, natural latex, jute, leather, wool felt, copper and brass are among our most important materials.</li>
</ul>', 100, 1,1, CAST(N'2022-09-20T12:45:43.950' AS DateTime), 1, 4, 2)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Burrow Sport Slip On', N'20220920125557115.jpg', N'20220920125557115', 99, 0, N'<p>Cozy up to a campfire, an adventure or while at home in the Women''s Nike Burrow Sport Slip On.</p>
<ul>
<li>Synthetic upper in a sport slip on style with a round toe</li>
<li>Slip on entry</li>
<li>Embroidered details</li>
<li>Zipper pocket on the top lets you stash small items like keys or cash and makes a playful nod to Nike Tech Fleece</li>
<li>Fleece-like material lines the inside (and sockliner) for added warmth and comfort</li>
<li>Outsole traction pattern helps on a variety of surfaces</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T12:55:57.567' AS DateTime), 1, 1, 4)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s BOBS For Dogs Too Cozy Slipper', N'20220920130052600.jpg', N'20220920130052600', 44, 10, N'<p>Stay cozy in the Women''s Skechers BOBS For Dogs Too Cozy Slipper.</p>
<ul>
<li>Microfiber fabric upper in a mule slipper style with a round toe</li>
<li>Slip-on entry</li>
<li>Full backed slipper for added warmth</li>
<li>Soft plush faux-fur collar trim and lining</li>
<li>Memory foam cushioned insole</li>
<li>Flexible traction outsole</li>
</ul>', 100, 1, 0, CAST(N'2022-09-20T13:00:52.867' AS DateTime), 1, 5, 4)
INSERT [dbo].[Products] ([ProductName], [Image], [ImageList], [OriginalPrice], [PromotionPercent], [Description], [Quantity], [Status], [ProductGenderCategory], [DateCreate], [AdminId], [ManufactureId], [CatalogId]) VALUES (N'Women''s Britt Trapper Slipper', N'20220920130630194.jpg', N'20220920130630194', 44, 10, N'<p>Rest comfortably indoors or out in the moccasin style Minnetonka Britt Trapper Slipper. Charming features include a decorative leather ribbon that ties in a bow, rustic stitching at toe and heel seems and luxurious faux fur lining with a cushioned insole.<br /><br /></p>
<ul>
<li>Suede or fabric upper with round moc toe</li>
<li>Decorative laces, padded collar, cushioned insole</li>
<li>Traction grippy indoor/outdoor outsole</li>
<li>Liner on charcoal slipper may be slightly different than pictured</li>
</ul>', 98, 1, 0, CAST(N'2022-09-20T13:06:30.420' AS DateTime), 1, 5, 3)


INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (1, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (1, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (1, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (1, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (2, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (2, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (2, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (2, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (3, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (3, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (3, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (3, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (4, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (4, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (4, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (4, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (5, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (5, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (5, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (5, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (6, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (6, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (6, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (6, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (7, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (7, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (7, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (7, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (8, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (8, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (8, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (8, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (9, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (9, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (9, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (9, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (10, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (10, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (10, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (10, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (11, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (11, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (11, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (11, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (12, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (12, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (12, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (12, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (13, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (13, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (13, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (13, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (14, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (14, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (14, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (14, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (15, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (15, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (15, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (15, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (16, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (16, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (16, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (16, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (17, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (17, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (17, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (17, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (18, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (18, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (18, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (18, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (19, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (19, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (19, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (19, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (20, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (20, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (20, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (20, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (21, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (21, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (21, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (21, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (22, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (22, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (22, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (22, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (23, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (23, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (23, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (23, 4, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (24, 1, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (24, 2, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (24, 3, 1, NULL)
INSERT [dbo].[ProductAttributes] ([ProductId], [AttributeValueId], [Status], [Note]) VALUES (24, 4, 1, NULL)
