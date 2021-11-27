use AppDb;
go
INSERT Users (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin) Values
			('sa', 'sa', 'System', 'Admin', '911', 'help@system.com', 1, 1),
			('rv', 'rv', 'System', 'Reviewer', '811', 'help@reviewer.com', 1, 0),
			('us1', 'us', 'System', 'User1', '411', 'help@user.com', 0, 0);
go
INSERT Vendors (Code, Name, Address, City, State, Zip, Phone, Email) Values
			('AMAZ', 'Amazon', '1 Amazon Way', 'Seattle', 'WA', '98765', '800-CAL-AMAZ', 'info@amazon.com'),
			('TARG', 'Target', '655 Target Dr', 'Minneapolis', 'MN', '77655', '800-CAL-TARG', 'info@target.com'),
			('BBUY', 'BestBuy', '433 Good Buy Ct', 'Atlanta', 'GA', '34746', '800-CAL-BBUY', 'info@bestbuy.com');
go
DECLARE @amaz int = 0;
SELECT @amaz = Id From Vendors Where Code = 'AMAZ';
INSERT Products (PartNbr, Name, Price, Unit, VendorId) Values
			('ECHO', 'Echo', 100, 'Each', @amaz),
			('ECHODOT', 'Echo Dot', 40, 'Each', @amaz),
			('ECHOSHOW5', 'Echo Show 5', 120, 'Each', @amaz),
			('ECHOSHOW8', 'Echo Show 8', 100, 'Each', @amaz),
			('FIRESTK', 'Fire Stick', 50, 'Each', @amaz),
			('FIRETV', 'Fire TV', 150, 'Each', @amaz);
go
DECLARE @targ int = 0;
SELECT @targ = Id From Vendors Where Code = 'TARG';
INSERT Products (PartNbr, Name, Price, Unit, VendorId) Values
			('REAM', 'Paper Ream', 5, 'Each', @targ),
			('CASE', 'Paper Case 10 Reams', 50, 'Each', @targ),
			('PEN', 'Ballpoint Pens 30', 15, 'Each', @targ),
			('PENCIL', 'Pencils 50cnt', 10, 'Each', @targ),
			('FOLDER', 'Pocket folders 5cnd', 20, 'Each', @targ);
go
DECLARE @bbuy int = 0;
SELECT @bbuy = Id From Vendors Where Code = 'BBUY';
INSERT Products (PartNbr, Name, Price, Unit, VendorId) Values
			('IPHONE13', 'Apple iPhone 13', 750, 'Each', @bbuy),
			('IPAD', 'Apple iPad 64', 450, 'Each', @bbuy),
			('MACBOOK', 'Apple Macbook Pro', 2750, 'Each', @bbuy);
go
