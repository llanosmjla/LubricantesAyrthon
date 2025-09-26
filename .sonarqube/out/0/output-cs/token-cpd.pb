£
dC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Interfaces\ISellerService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )

Interfaces) 3
{ 
public 

	interface 
ISellerService #
:$ %
IBaseService& 2
<3 4
SellerCreateDto4 C
,C D
SellerReadDtoE R
,R S
SellerUpdateDtoT c
>c d
{ 
}		 
}

 Å
bC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Interfaces\ISaleService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )

Interfaces) 3
{ 
public 

	interface 
ISaleService !
:" #
IBaseService$ 0
<0 1
SaleCreateDto1 >
,> ?
SaleReadDto@ K
,K L
SaleUpdateDtoM Z
>Z [
{ 
public		 
decimal		 
CalculateTotalPrice		 *
(		* +
IEnumerable		+ 6
<		6 7
SaleDetailCreateDto		7 J
>		J K
saleDetails		L W
)		W X
;		X Y
}

 
} ®
eC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Interfaces\IProductService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )

Interfaces) 3
{ 
public 

	interface 
IProductService $
:% &
IBaseService' 3
<3 4
ProductCreateDto4 D
,D E
ProductReadDtoF T
,T U
ProductUpdateDtoV f
>f g
{ 
} 
} ≠
fC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Interfaces\ICustomerService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )

Interfaces) 3
{ 
public 

	interface 
ICustomerService %
:& '
IBaseService( 4
<4 5
CustomerCreateDto5 F
,F G
CustomerReadDtoH W
,W X
CustomerUpdateDtoY j
>j k
{		 
}

 
} Ü
bC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Interfaces\IBaseService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )

Interfaces) 3
{ 
public 

	interface 
IBaseService !
<! "
TCreate" )
,) *
TRead+ 0
,0 1
TUpdate2 9
>9 :
{ 
Task 
< 
IEnumerable 
< 
TRead 
> 
>  
GetAllAsync! ,
(, -
)- .
;. /
Task 
< 
TRead 
> 
GetByIdAsync  
(  !
int! $
id% '
)' (
;( )
Task 
< 
TRead 
> 
CreateAsync 
(  
TCreate  '
entity( .
). /
;/ 0
Task 
< 
TRead 
> 
UpdateAsync 
(  
int  #
id$ &
,& '
TUpdate( /
entity0 6
)6 7
;7 8
Task		 
<		 
bool		 
>		 
DeleteAsync		 
(		 
int		 "
id		# %
)		% &
;		& '
}

 
}  F
oC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Implementations\Seller\SellerService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )
Implementations) 8
{ 
public		 

class		 
SellerService		 
:		  
ISellerService		! /
{

 
private 
readonly 
IBaseRepository (
<( )
Seller) /
>/ 0
_sellerRepository1 B
;B C
public 
SellerService 
( 
IBaseRepository ,
<, -
Seller- 3
>3 4
sellerRepository5 E
)E F
{ 	
_sellerRepository 
= 
sellerRepository  0
;0 1
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
SellerReadDto& 3
>3 4
>4 5
GetAllAsync6 A
(A B
)B C
{ 	
var 
sellers 
= 
await 
_sellerRepository  1
.1 2
GetAllAsync2 =
(= >
)> ?
;? @
return 
sellers 
. 
Select !
(! "
s" #
=>$ &
new' *
SellerReadDto+ 8
{ 
Id 
= 
s 
. 
Id 
, 
Name 
= 
s 
. 
Name 
, 
Age 
= 
s 
. 
Age 
, 
Email 
= 
s 
. 
Email 
,  
Phone 
= 
s 
. 
Phone 
,  
Address 
= 
s 
. 
Address #
,# $
Salary 
= 
s 
. 
Salary !
} 
) 
; 
}   	
public"" 
async"" 
Task"" 
<"" 
SellerReadDto"" '
>""' (
GetByIdAsync"") 5
(""5 6
int""6 9
id"": <
)""< =
{## 	
var$$ 
seller$$ 
=$$ 
await$$ 
_sellerRepository$$ 0
.$$0 1
GetByIdAsync$$1 =
($$= >
id$$> @
)$$@ A
;$$A B
if%% 
(%% 
seller%% 
==%% 
null%% 
)%% 
return%%  &
null%%' +
;%%+ ,
return'' 
new'' 
SellerReadDto'' $
{(( 
Id)) 
=)) 
seller)) 
.)) 
Id)) 
,)) 
Name** 
=** 
seller** 
.** 
Name** "
,**" #
Age++ 
=++ 
seller++ 
.++ 
Age++  
,++  !
Email,, 
=,, 
seller,, 
.,, 
Email,, $
,,,$ %
Phone-- 
=-- 
seller-- 
.-- 
Phone-- $
,--$ %
Address.. 
=.. 
seller..  
...  !
Address..! (
,..( )
Salary// 
=// 
seller// 
.//  
Salary//  &
}00 
;00 
}11 	
public33 
async33 
Task33 
<33 
SellerReadDto33 '
>33' (
CreateAsync33) 4
(334 5
SellerCreateDto335 D
entity33E K
)33K L
{44 	
var55 
seller55 
=55 
new55 
Seller55 #
{66 
Ci77 
=77 
entity77 
.77 
Ci77 
,77 
Name88 
=88 
entity88 
.88 
Name88 "
,88" #
Age99 
=99 
entity99 
.99 
Age99  
,99  !
Email:: 
=:: 
entity:: 
.:: 
Email:: $
,::$ %
Phone;; 
=;; 
entity;; 
.;; 
Phone;; $
,;;$ %
Address<< 
=<< 
entity<<  
.<<  !
Address<<! (
,<<( )
Salary== 
=== 
entity== 
.==  
Salary==  &
}>> 
;>> 
await@@ 
_sellerRepository@@ #
.@@# $
AddAsync@@$ ,
(@@, -
seller@@- 3
)@@3 4
;@@4 5
returnBB 
newBB 
SellerReadDtoBB $
{CC 
IdDD 
=DD 
sellerDD 
.DD 
IdDD 
,DD 
NameEE 
=EE 
sellerEE 
.EE 
NameEE "
,EE" #
AgeFF 
=FF 
sellerFF 
.FF 
AgeFF  
,FF  !
EmailGG 
=GG 
sellerGG 
.GG 
EmailGG $
,GG$ %
PhoneHH 
=HH 
sellerHH 
.HH 
PhoneHH $
,HH$ %
AddressII 
=II 
sellerII  
.II  !
AddressII! (
,II( )
SalaryJJ 
=JJ 
sellerJJ 
.JJ  
SalaryJJ  &
}KK 
;KK 
}NN 	
publicPP 
asyncPP 
TaskPP 
<PP 
SellerReadDtoPP '
>PP' (
UpdateAsyncPP) 4
(PP4 5
intPP5 8
idPP9 ;
,PP; <
SellerUpdateDtoPP= L
entityPPM S
)PPS T
{QQ 	
varRR 
SellerRR 
=RR 
awaitRR 
_sellerRepositoryRR 0
.RR0 1
GetByIdAsyncRR1 =
(RR= >
idRR> @
)RR@ A
;RRA B
ifSS 
(SS 
SellerSS 
==SS 
nullSS 
)SS 
returnSS  &
nullSS' +
;SS+ ,
SellerUU 
.UU 
NameUU 
=UU 
entityUU  
.UU  !
NameUU! %
;UU% &
SellerVV 
.VV 
AgeVV 
=VV 
entityVV 
.VV  
AgeVV  #
;VV# $
SellerWW 
.WW 
EmailWW 
=WW 
entityWW !
.WW! "
EmailWW" '
;WW' (
SellerXX 
.XX 
PhoneXX 
=XX 
entityXX !
.XX! "
PhoneXX" '
;XX' (
SellerYY 
.YY 
AddressYY 
=YY 
entityYY #
.YY# $
AddressYY$ +
;YY+ ,
SellerZZ 
.ZZ 
SalaryZZ 
=ZZ 
entityZZ "
.ZZ" #
SalaryZZ# )
;ZZ) *
var\\ 
updatedSeller\\ 
=\\ 
await\\  %
_sellerRepository\\& 7
.\\7 8
UpdateAsync\\8 C
(\\C D
id\\D F
,\\F G
Seller\\H N
)\\N O
;\\O P
return]] 
new]] 
SellerReadDto]] $
{^^ 
Id__ 
=__ 
updatedSeller__ "
.__" #
Id__# %
,__% &
Name`` 
=`` 
updatedSeller`` $
.``$ %
Name``% )
,``) *
Ageaa 
=aa 
updatedSelleraa #
.aa# $
Ageaa$ '
,aa' (
Emailbb 
=bb 
updatedSellerbb %
.bb% &
Emailbb& +
,bb+ ,
Phonecc 
=cc 
updatedSellercc %
.cc% &
Phonecc& +
,cc+ ,
Addressdd 
=dd 
updatedSellerdd '
.dd' (
Addressdd( /
,dd/ 0
Salaryee 
=ee 
updatedSelleree &
.ee& '
Salaryee' -
}ff 
;ff 
}gg 	
publicii 
asyncii 
Taskii 
<ii 
boolii 
>ii 
DeleteAsyncii  +
(ii+ ,
intii, /
idii0 2
)ii2 3
{jj 	
varkk 
Sellerkk 
=kk 
awaitkk 
_sellerRepositorykk 0
.kk0 1
GetByIdAsynckk1 =
(kk= >
idkk> @
)kk@ A
;kkA B
ifll 
(ll 
Sellerll 
==ll 
nullll 
)ll 
returnll  &
falsell' ,
;ll, -
awaitnn 
_sellerRepositorynn #
.nn# $
DeleteAsyncnn$ /
(nn/ 0
idnn0 2
)nn2 3
;nn3 4
returnoo 
trueoo 
;oo 
}pp 	
}uu 
}vv ¥d
kC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Implementations\Sale\SaleService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )
Implementations) 8
{ 
public 

class 
SaleService 
: 
ISaleService +
{		 
private

 
readonly

 
IBaseRepository

 (
<

( )
Sale

) -
>

- .
_saleRepository

/ >
;

> ?
public 
SaleService 
( 
IBaseRepository *
<* +
Sale+ /
>/ 0
saleRepository1 ?
)? @
{ 	
_saleRepository 
= 
saleRepository ,
;, -
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
SaleReadDto& 1
>1 2
>2 3
GetAllAsync4 ?
(? @
)@ A
{ 	
var 
sales 
= 
await 
_saleRepository -
.- .
GetAllAsync. 9
(9 :
): ;
;; <
return 
sales 
. 
Select 
(  
s  !
=>" $
new% (
SaleReadDto) 4
{ 
Id 
= 
s 
. 
Id 
, 

IdCustomer 
= 
s 
. 

IdCustomer )
,) *
IdSeller 
= 
s 
. 
IdSeller %
,% &

TotalPrice 
= 
s 
. 

TotalPrice )
,) *
SaleDate 
= 
s 
. 
SaleDate %
,% &
SaleDetails 
= 
s 
.  
SaleDetails  +
?+ ,
., -
Select- 3
(3 4
sd4 6
=>7 9
new: =
SaleDetailReadDto> O
{ 
Id 
= 
sd 
. 
Id 
, 
	IdProduct 
= 
sd  "
." #
	IdProduct# ,
,, -
Quantity 
= 
sd !
.! "
Quantity" *
,* +
	UnitPrice   
=   
sd    "
.  " #
	UnitPrice  # ,
}!! 
)!! 
.!! 
ToList!! 
(!! 
)!! 
}"" 
)"" 
;"" 
}## 	
public%% 
async%% 
Task%% 
<%% 
SaleReadDto%% %
>%%% &
GetByIdAsync%%' 3
(%%3 4
int%%4 7
id%%8 :
)%%: ;
{&& 	
var'' 
sale'' 
='' 
await'' 
_saleRepository'' ,
.'', -
GetByIdAsync''- 9
(''9 :
id'': <
)''< =
;''= >
if(( 
((( 
sale(( 
==(( 
null(( 
)(( 
return(( $
null((% )
;(() *
return** 
new** 
SaleReadDto** "
{++ 
Id,, 
=,, 
sale,, 
.,, 
Id,, 
,,, 

IdCustomer-- 
=-- 
sale-- !
.--! "

IdCustomer--" ,
,--, -
IdSeller.. 
=.. 
sale.. 
...  
IdSeller..  (
,..( )

TotalPrice// 
=// 
sale// !
.//! "

TotalPrice//" ,
,//, -
SaleDate00 
=00 
sale00 
.00  
SaleDate00  (
,00( )
SaleDetails11 
=11 
sale11 "
.11" #
SaleDetails11# .
?11. /
.11/ 0
Select110 6
(116 7
sd117 9
=>11: <
new11= @
SaleDetailReadDto11A R
{22 
Id33 
=33 
sd33 
.33 
Id33 
,33 
	IdProduct44 
=44 
sd44  "
.44" #
	IdProduct44# ,
,44, -
Quantity55 
=55 
sd55 !
.55! "
Quantity55" *
,55* +
	UnitPrice66 
=66 
sd66  "
.66" #
	UnitPrice66# ,
}77 
)77 
.77 
ToList77 
(77 
)77 
}88 
;88 
}99 	
public;; 
async;; 
Task;; 
<;; 
SaleReadDto;; %
>;;% &
CreateAsync;;' 2
(;;2 3
SaleCreateDto;;3 @
entity;;A G
);;G H
{<< 	
var?? 
sale?? 
=?? 
new?? 
Sale?? 
{@@ 

IdCustomerAA 
=AA 
entityAA #
.AA# $

IdCustomerAA$ .
,AA. /
IdSellerBB 
=BB 
entityBB !
.BB! "
IdSellerBB" *
,BB* +

TotalPriceCC 
=CC 
CalculateTotalPriceCC 0
(CC0 1
entityCC1 7
.CC7 8
SaleDetailsCC8 C
!CCC D
)CCD E
,CCE F
SaleDateDD 
=DD 
entityDD !
.DD! "
SaleDateDD" *
,DD* +
SaleDetailsEE 
=EE 
entityEE $
.EE$ %
SaleDetailsEE% 0
.EE0 1
SelectEE1 7
(EE7 8
sdEE8 :
=>EE; =
newEE> A

SaleDetailEEB L
{FF 
	IdProductGG 
=GG 
sdGG  "
.GG" #
	IdProductGG# ,
,GG, -
QuantityHH 
=HH 
sdHH !
.HH! "
QuantityHH" *
,HH* +
	UnitPriceII 
=II 
sdII  "
.II" #
	UnitPriceII# ,
}JJ 
)JJ 
.JJ 
ToListJJ 
(JJ 
)JJ 
}KK 
;KK 
awaitMM 
_saleRepositoryMM !
.MM! "
AddAsyncMM" *
(MM* +
saleMM+ /
)MM/ 0
;MM0 1
returnNN 
newNN 
SaleReadDtoNN "
{OO 
IdPP 
=PP 
salePP 
.PP 
IdPP 
,PP 

IdCustomerQQ 
=QQ 
saleQQ !
.QQ! "

IdCustomerQQ" ,
,QQ, -
IdSellerRR 
=RR 
saleRR 
.RR  
IdSellerRR  (
,RR( )

TotalPriceSS 
=SS 
saleSS !
.SS! "

TotalPriceSS" ,
,SS, -
SaleDateTT 
=TT 
saleTT 
.TT  
SaleDateTT  (
,TT( )
SaleDetailsUU 
=UU 
saleUU "
.UU" #
SaleDetailsUU# .
?UU. /
.UU/ 0
SelectUU0 6
(UU6 7
sdUU7 9
=>UU: <
newUU= @
SaleDetailReadDtoUUA R
{VV 
IdWW 
=WW 
sdWW 
.WW 
IdWW 
,WW 
	IdProductXX 
=XX 
sdXX  "
.XX" #
	IdProductXX# ,
,XX, -
QuantityYY 
=YY 
sdYY !
.YY! "
QuantityYY" *
,YY* +
	UnitPriceZZ 
=ZZ 
sdZZ  "
.ZZ" #
	UnitPriceZZ# ,
}[[ 
)[[ 
.[[ 
ToList[[ 
([[ 
)[[ 
}\\ 
;\\ 
}]] 	
public__ 
async__ 
Task__ 
<__ 
SaleReadDto__ %
>__% &
UpdateAsync__' 2
(__2 3
int__3 6
id__7 9
,__9 :
SaleUpdateDto__; H
entity__I O
)__O P
{`` 	
varaa 
saleaa 
=aa 
awaitaa 
_saleRepositoryaa ,
.aa, -
GetByIdAsyncaa- 9
(aa9 :
idaa: <
)aa< =
;aa= >
ifbb 
(bb 
salebb 
==bb 
nullbb 
)bb 
returnbb $
nullbb% )
;bb) *
saledd 
.dd 

IdCustomerdd 
=dd 
entitydd $
.dd$ %

IdCustomerdd% /
;dd/ 0
saleee 
.ee 
IdSelleree 
=ee 
entityee "
.ee" #
IdSelleree# +
;ee+ ,
saleff 
.ff 

TotalPriceff 
=ff 
entityff $
.ff$ %

TotalPriceff% /
;ff/ 0
salegg 
.gg 
SaleDategg 
=gg 
entitygg "
.gg" #
SaleDategg# +
;gg+ ,
salehh 
.hh 
SaleDetailshh 
=hh 
entityhh %
.hh% &
SaleDetailshh& 1
.hh1 2
Selecthh2 8
(hh8 9
sdhh9 ;
=>hh< >
newhh? B

SaleDetailhhC M
{ii 
	IdProductjj 
=jj 
sdjj 
.jj 
	IdProductjj (
,jj( )
Quantitykk 
=kk 
sdkk 
.kk 
Quantitykk &
,kk& '
	UnitPricell 
=ll 
sdll 
.ll 
	UnitPricell (
}mm 
)mm 
.mm 
ToListmm 
(mm 
)mm 
;mm 
varoo 
saleUpdatedoo 
=oo 
awaitoo #
_saleRepositoryoo$ 3
.oo3 4
UpdateAsyncoo4 ?
(oo? @
idoo@ B
,ooB C
saleooD H
)ooH I
;ooI J
returnqq 
newqq 
SaleReadDtoqq "
{rr 
Idss 
=ss 
saleUpdatedss  
.ss  !
Idss! #
,ss# $

IdCustomertt 
=tt 
saleUpdatedtt (
.tt( )

IdCustomertt) 3
,tt3 4
IdSelleruu 
=uu 
saleUpdateduu &
.uu& '
IdSelleruu' /
,uu/ 0

TotalPricevv 
=vv 
saleUpdatedvv (
.vv( )

TotalPricevv) 3
,vv3 4
SaleDateww 
=ww 
saleUpdatedww &
.ww& '
SaleDateww' /
,ww/ 0
SaleDetailsxx 
=xx 
saleUpdatedxx )
.xx) *
SaleDetailsxx* 5
?xx5 6
.xx6 7
Selectxx7 =
(xx= >
sdxx> @
=>xxA C
newxxD G
SaleDetailReadDtoxxH Y
{yy 
Idzz 
=zz 
sdzz 
.zz 
Idzz 
,zz 
	IdProduct{{ 
={{ 
sd{{  "
.{{" #
	IdProduct{{# ,
,{{, -
Quantity|| 
=|| 
sd|| !
.||! "
Quantity||" *
,||* +
	UnitPrice}} 
=}} 
sd}}  "
.}}" #
	UnitPrice}}# ,
}~~ 
)~~ 
.~~ 
ToList~~ 
(~~ 
)~~ 
} 
; 
}
ÄÄ 	
public
ÇÇ 
async
ÇÇ 
Task
ÇÇ 
<
ÇÇ 
bool
ÇÇ 
>
ÇÇ 
DeleteAsync
ÇÇ  +
(
ÇÇ+ ,
int
ÇÇ, /
id
ÇÇ0 2
)
ÇÇ2 3
{
ÉÉ 	
var
ÑÑ 
sale
ÑÑ 
=
ÑÑ 
await
ÑÑ 
_saleRepository
ÑÑ ,
.
ÑÑ, -
GetByIdAsync
ÑÑ- 9
(
ÑÑ9 :
id
ÑÑ: <
)
ÑÑ< =
;
ÑÑ= >
if
ÖÖ 
(
ÖÖ 
sale
ÖÖ 
==
ÖÖ 
null
ÖÖ 
)
ÖÖ 
return
ÖÖ $
false
ÖÖ% *
;
ÖÖ* +
await
áá 
_saleRepository
áá !
.
áá! "
DeleteAsync
áá" -
(
áá- .
id
áá. 0
)
áá0 1
;
áá1 2
return
àà 
true
àà 
;
àà 
}
ââ 	
public
ãã 
decimal
ãã !
CalculateTotalPrice
ãã *
(
ãã* +
IEnumerable
ãã+ 6
<
ãã6 7!
SaleDetailCreateDto
ãã7 J
>
ããJ K
saleDetails
ããL W
)
ããW X
{
åå 	
return
çç 
saleDetails
çç 
.
çç 
Sum
çç "
(
çç" #
sd
çç# %
=>
çç& (
sd
çç) +
.
çç+ ,
Quantity
çç, 4
*
çç5 6
sd
çç7 9
.
çç9 :
	UnitPrice
çç: C
)
ççC D
;
ççD E
}
éé 	
}
èè 
}êê Ã>
qC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Implementations\Product\ProductService.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )
Implementations) 8
{		 
public

 

class

 
ProductService

 
:

  !
IProductService

" 1
{ 
private 
readonly 
IBaseRepository (
<( )
Product) 0
>0 1
_repository2 =
;= >
public 
ProductService 
( 
IBaseRepository -
<- .
Product. 5
>5 6

repository7 A
)A B
{ 	
_repository 
= 

repository $
;$ %
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
ProductReadDto& 4
>4 5
>5 6
GetAllAsync7 B
(B C
)C D
{ 	
var 
products 
= 
await  
_repository! ,
., -
GetAllAsync- 8
(8 9
)9 :
;: ;
return 
products 
. 
Select "
(" #
p# $
=>% '
new( +
ProductReadDto, :
{ 
Id 
= 
p 
. 
Id 
, 
Name 
= 
p 
. 
Name 
, 
Description 
= 
p 
.  
Description  +
,+ ,
Price 
= 
p 
. 
Price 
,  
Stock 
= 
p 
. 
Stock 
} 
) 
; 
} 	
public   
async   
Task   
<   
ProductReadDto   (
>  ( )
GetByIdAsync  * 6
(  6 7
int  7 :
id  ; =
)  = >
{!! 	
var"" 
product"" 
="" 
await"" 
_repository""  +
.""+ ,
GetByIdAsync"", 8
(""8 9
id""9 ;
)""; <
;""< =
if## 
(## 
product## 
==## 
null## 
)##  
throw##! &
new##' * 
KeyNotFoundException##+ ?
(##? @
$"##@ B
$str##B O
{##O P
id##P R
}##R S
$str##S a
"##a b
)##b c
;##c d
return%% 
new%% 
ProductReadDto%% %
{&& 
Id'' 
='' 
product'' 
.'' 
Id'' 
,''  
Name(( 
=(( 
product(( 
.(( 
Name(( #
,((# $
Description)) 
=)) 
product)) %
.))% &
Description))& 1
,))1 2
Price** 
=** 
product** 
.**  
Price**  %
,**% &
Stock++ 
=++ 
product++ 
.++  
Stock++  %
},, 
;,, 
}-- 	
public.. 
async.. 
Task.. 
<.. 
ProductReadDto.. (
>..( )
CreateAsync..* 5
(..5 6
ProductCreateDto..6 F
product..G N
)..N O
{// 	
var11 

newProduct11 
=11 
new11  
Product11! (
{22 
Name33 
=33 
product33 
.33 
Name33 #
,33# $
Description44 
=44 
product44 %
.44% &
Description44& 1
,441 2
Price55 
=55 
product55 
.55  
Price55  %
,55% &
Stock66 
=66 
product66 
.66  
Stock66  %
}77 
;77 
var99 
createdProduct99 
=99  
await99! &
_repository99' 2
.992 3
AddAsync993 ;
(99; <

newProduct99< F
)99F G
;99G H
return;; 
new;; 
ProductReadDto;; %
{<< 
Id== 
=== 
createdProduct== #
.==# $
Id==$ &
,==& '
Name>> 
=>> 
createdProduct>> %
.>>% &
Name>>& *
,>>* +
Description?? 
=?? 
createdProduct?? ,
.??, -
Description??- 8
,??8 9
Price@@ 
=@@ 
createdProduct@@ &
.@@& '
Price@@' ,
,@@, -
StockAA 
=AA 
createdProductAA &
.AA& '
StockAA' ,
}BB 
;BB 
}DD 	
publicFF 
asyncFF 
TaskFF 
<FF 
ProductReadDtoFF (
>FF( )
UpdateAsyncFF* 5
(FF5 6
intFF6 9
idFF: <
,FF< =
ProductUpdateDtoFF> N
productFFO V
)FFV W
{GG 	
varHH 
existingProductHH 
=HH  !
awaitHH" '
_repositoryHH( 3
.HH3 4
GetByIdAsyncHH4 @
(HH@ A
idHHA C
)HHC D
;HHD E
ifII 
(II 
existingProductII 
==II  "
nullII# '
)II' (
returnII) /
nullII0 4
;II4 5
existingProductKK 
.KK 
NameKK  
=KK! "
productKK# *
.KK* +
NameKK+ /
;KK/ 0
existingProductLL 
.LL 
DescriptionLL '
=LL( )
productLL* 1
.LL1 2
DescriptionLL2 =
;LL= >
existingProductMM 
.MM 
PriceMM !
=MM" #
productMM$ +
.MM+ ,
PriceMM, 1
;MM1 2
existingProductNN 
.NN 
StockNN !
=NN" #
productNN$ +
.NN+ ,
StockNN, 1
;NN1 2
varPP 
updatedProductPP 
=PP  
awaitPP! &
_repositoryPP' 2
.PP2 3
UpdateAsyncPP3 >
(PP> ?
idPP? A
,PPA B
existingProductPPC R
)PPR S
;PPS T
returnRR 
newRR 
ProductReadDtoRR %
{SS 
IdTT 
=TT 
updatedProductTT #
.TT# $
IdTT$ &
,TT& '
NameUU 
=UU 
updatedProductUU %
.UU% &
NameUU& *
,UU* +
DescriptionVV 
=VV 
updatedProductVV ,
.VV, -
DescriptionVV- 8
,VV8 9
PriceWW 
=WW 
updatedProductWW &
.WW& '
PriceWW' ,
,WW, -
StockXX 
=XX 
updatedProductXX &
.XX& '
StockXX' ,
}YY 
;YY 
}ZZ 	
public\\ 
async\\ 
Task\\ 
<\\ 
bool\\ 
>\\ 
DeleteAsync\\  +
(\\+ ,
int\\, /
id\\0 2
)\\2 3
{]] 	
var^^ 
product^^ 
=^^ 
await^^ 
_repository^^  +
.^^+ ,
GetByIdAsync^^, 8
(^^8 9
id^^9 ;
)^^; <
;^^< =
if__ 
(__ 
product__ 
==__ 
null__ 
)__  
return__! '
false__( -
;__- .
awaitaa 
_repositoryaa 
.aa 
DeleteAsyncaa )
(aa) *
idaa* ,
)aa, -
;aa- .
returncc 
truecc 
;cc 
}dd 	
}ff 
}gg ÜB
sC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Services\Implementations\Customer\CustomerService.cs
	namespace		 	!
LubricantesAyrthonAPI		
 
.		  
Services		  (
.		( )
Implementations		) 8
{

 
public 

class 
CustomerService  
:! "
ICustomerService# 3
{ 
private 
readonly 
IBaseRepository (
<( )
Customer) 1
>1 2
_customerRepository3 F
;F G
public 
CustomerService 
( 
IBaseRepository .
<. /
Customer/ 7
>7 8
customerRepository9 K
)K L
{ 	
_customerRepository 
=  !
customerRepository" 4
;4 5
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
CustomerReadDto& 5
>5 6
>6 7
GetAllAsync8 C
(C D
)D E
{ 	
var 
	customers 
= 
await !
_customerRepository" 5
.5 6
GetAllAsync6 A
(A B
)B C
;C D
return 
	customers 
. 
Select #
(# $
c$ %
=>& (
new) ,
CustomerReadDto- <
{ 
Id 
= 
c 
. 
Id 
, 
Ci 
= 
c 
. 
Ci 
, 
Name 
= 
c 
. 
Name 
, 
Email   
=   
c   
.   
Email   
,    
Phone!! 
=!! 
c!! 
.!! 
Phone!! 
,!!  
Address"" 
="" 
c"" 
."" 
Address"" #
}## 
)## 
;## 
}$$ 	
public&& 
async&& 
Task&& 
<&& 
CustomerReadDto&& )
>&&) *
GetByIdAsync&&+ 7
(&&7 8
int&&8 ;
id&&< >
)&&> ?
{'' 	
var(( 
customer(( 
=(( 
await((  
_customerRepository((! 4
.((4 5
GetByIdAsync((5 A
(((A B
id((B D
)((D E
;((E F
if)) 
()) 
customer)) 
==)) 
null))  
)))  !
throw))" '
new))( + 
KeyNotFoundException)), @
())@ A
$"))A C
$str))C O
{))O P
id))P R
}))R S
$str))S a
"))a b
)))b c
;))c d
return++ 
new++ 
CustomerReadDto++ &
{,, 
Id-- 
=-- 
customer-- 
.-- 
Id--  
,--  !
Ci.. 
=.. 
customer.. 
... 
Ci..  
,..  !
Name// 
=// 
customer// 
.//  
Name//  $
,//$ %
Email00 
=00 
customer00  
.00  !
Email00! &
}11 
;11 
}22 	
public44 
async44 
Task44 
<44 
CustomerReadDto44 )
>44) *
CreateAsync44+ 6
(446 7
CustomerCreateDto447 H
customerCreateDto44I Z
)44Z [
{55 	
var66 
newCustomer66 
=66 
new66 !
Customer66" *
{77 
Ci88 
=88 
customerCreateDto88 &
.88& '
Ci88' )
,88) *
Name99 
=99 
customerCreateDto99 (
.99( )
Name99) -
,99- .
Email:: 
=:: 
customerCreateDto:: )
.::) *
Email::* /
,::/ 0
Phone;; 
=;; 
customerCreateDto;; )
.;;) *
Phone;;* /
,;;/ 0
Address<< 
=<< 
customerCreateDto<< +
.<<+ ,
Address<<, 3
}>> 
;>> 
var@@ 
createdCustomer@@ 
=@@  !
await@@" '
_customerRepository@@( ;
.@@; <
AddAsync@@< D
(@@D E
newCustomer@@E P
)@@P Q
;@@Q R
returnMM 
newMM 
CustomerReadDtoMM &
{NN 
IdOO 
=OO 
createdCustomerOO $
.OO$ %
IdOO% '
,OO' (
CiPP 
=PP 
createdCustomerPP $
.PP$ %
CiPP% '
,PP' (
NameQQ 
=QQ 
createdCustomerQQ &
.QQ& '
NameQQ' +
,QQ+ ,
EmailRR 
=RR 
createdCustomerRR '
.RR' (
EmailRR( -
,RR- .
PhoneSS 
=SS 
createdCustomerSS '
.SS' (
PhoneSS( -
,SS- .
AddressTT 
=TT 
createdCustomerTT )
.TT) *
AddressTT* 1
}UU 
;UU 
}VV 	
publicXX 
asyncXX 
TaskXX 
<XX 
CustomerReadDtoXX )
>XX) *
UpdateAsyncXX+ 6
(XX6 7
intXX7 :
idXX; =
,XX= >
CustomerUpdateDtoXX? P
customerXXQ Y
)XXY Z
{YY 	
varZZ 
existingCustomerZZ  
=ZZ! "
awaitZZ# (
_customerRepositoryZZ) <
.ZZ< =
GetByIdAsyncZZ= I
(ZZI J
idZZJ L
)ZZL M
;ZZM N
if[[ 
([[ 
existingCustomer[[  
==[[! #
null[[$ (
)[[( )
return[[* 0
null[[1 5
;[[5 6
existingCustomer]] 
.]] 
Name]] !
=]]" #
customer]]$ ,
.]], -
Name]]- 1
;]]1 2
existingCustomer^^ 
.^^ 
Email^^ "
=^^# $
customer^^% -
.^^- .
Email^^. 3
;^^3 4
existingCustomer__ 
.__ 
Phone__ "
=__# $
customer__% -
.__- .
Phone__. 3
;__3 4
existingCustomer`` 
.`` 
Address`` $
=``% &
customer``' /
.``/ 0
Address``0 7
;``7 8
varbb 
customerUpdatedbb 
=bb  !
awaitbb" '
_customerRepositorybb( ;
.bb; <
UpdateAsyncbb< G
(bbG H
idbbH J
,bbJ K
existingCustomerbbL \
)bb\ ]
;bb] ^
returncc 
newcc 
CustomerReadDtocc &
{dd 
Idee 
=ee 
customerUpdatedee $
.ee$ %
Idee% '
,ee' (
Ciff 
=ff 
customerUpdatedff $
.ff$ %
Ciff% '
,ff' (
Namegg 
=gg 
customerUpdatedgg &
.gg& '
Namegg' +
,gg+ ,
Emailhh 
=hh 
customerUpdatedhh '
.hh' (
Emailhh( -
,hh- .
Phoneii 
=ii 
customerUpdatedii '
.ii' (
Phoneii( -
,ii- .
Addressjj 
=jj 
customerUpdatedjj )
.jj) *
Addressjj* 1
}kk 
;kk 
}ll 	
publicnn 
asyncnn 
Tasknn 
<nn 
boolnn 
>nn 
DeleteAsyncnn  +
(nn+ ,
intnn, /
idnn0 2
)nn2 3
{oo 	
varpp 
customerpp 
=pp 
awaitpp  
_customerRepositorypp! 4
.pp4 5
GetByIdAsyncpp5 A
(ppA B
idppB D
)ppD E
;ppE F
ifqq 
(qq 
customerqq 
==qq 
nullqq  
)qq  !
returnqq" (
falseqq) .
;qq. /
awaitss 
_customerRepositoryss %
.ss% &
DeleteAsyncss& 1
(ss1 2
idss2 4
)ss4 5
;ss5 6
returntt 
truett 
;tt 
}uu 	
}vv 
}ww ã
iC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Repositories\Interfaces\IBaseRepository.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Repositories  ,
., -

Interfaces- 7
{ 
public 

	interface 
IBaseRepository $
<$ %
T% &
>& '
where( -
T. /
:0 1
class2 7
{ 
Task 
< 
IEnumerable 
< 
T 
> 
> 
GetAllAsync (
(( )
)) *
;* +
Task 
< 
T 
? 
> 
GetByIdAsync 
( 
int !
id" $
)$ %
;% &
Task		 
<		 
T		 
>		 
AddAsync		 
(		 
T		 
entity		 !
)		! "
;		" #
Task

 
<

 
T

 
?

 
>

 
UpdateAsync

 
(

 
int

  
id

! #
,

# $
T

% &
entity

' -
)

- .
;

. /
Task 
< 
bool 
> 
DeleteAsync 
( 
int "
id# %
)% &
;& '
} 
} ä#
oC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Repositories\Implementations\SellerRepository.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Repositories  ,
., -
Implementations- <
{		 
public

 

class

 
SellerRepository

 !
:

" #
IBaseRepository

$ 3
<

3 4
Seller

4 :
>

: ;
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
SellerRepository 
(  
AppDbContext  ,
context- 4
)4 5
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Seller& ,
>, -
>- .
GetAllAsync/ :
(: ;
); <
{ 	
return 
await 
_context !
.! "
Sellers" )
.) *
ToListAsync* 5
(5 6
)6 7
;7 8
} 	
public 
async 
Task 
< 
Seller  
?  !
>! "
GetByIdAsync# /
(/ 0
int0 3
id4 6
)6 7
{ 	
return 
await 
_context !
.! "
Sellers" )
.) *
	FindAsync* 3
(3 4
id4 6
)6 7
;7 8
} 	
public 
async 
Task 
< 
Seller !
>! "
AddAsync# +
(+ ,
Seller, 2
entity3 9
)9 :
{ 	
await 
_context 
. 
Sellers "
." #
AddAsync# +
(+ ,
entity, 2
)2 3
;3 4
await   
_context   
.   
SaveChangesAsync   +
(  + ,
)  , -
;  - .
return!! 
entity!! 
;!! 
}"" 	
public$$ 
async$$ 
Task$$ 
<$$ 
Seller$$  
?$$  !
>$$! "
UpdateAsync$$# .
($$. /
int$$/ 2
id$$3 5
,$$5 6
Seller$$7 =
entity$$> D
)$$D E
{%% 	
var&& 
seller&& 
=&& 
await&& 
_context&& '
.&&' (
Sellers&&( /
.&&/ 0
	FindAsync&&0 9
(&&9 :
id&&: <
)&&< =
;&&= >
if'' 
('' 
seller'' 
=='' 
null'' 
)'' 
return''  &
null''' +
;''+ ,
seller)) 
.)) 
Name)) 
=)) 
entity))  
.))  !
Name))! %
;))% &
seller** 
.** 
Email** 
=** 
entity** !
.**! "
Email**" '
;**' (
seller++ 
.++ 
Phone++ 
=++ 
entity++ !
.++! "
Phone++" '
;++' (
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
return.. 
seller.. 
;.. 
}// 	
public11 
async11 
Task11 
<11 
bool11 
>11 
DeleteAsync11  +
(11+ ,
int11, /
id110 2
)112 3
{22 	
var33 
seller33 
=33 
await33 
_context33 '
.33' (
Sellers33( /
.33/ 0
	FindAsync330 9
(339 :
id33: <
)33< =
;33= >
if44 
(44 
seller44 
==44 
null44 
)44 
return44  &
false44' ,
;44, -
_context66 
.66 
Sellers66 
.66 
Remove66 #
(66# $
seller66$ *
)66* +
;66+ ,
await77 
_context77 
.77 
SaveChangesAsync77 +
(77+ ,
)77, -
;77- .
return88 
true88 
;88 
}99 	
}:: 
};; ñ%
mC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Repositories\Implementations\SaleRepository.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Repositories  ,
., -
Implementations- <
{ 
public

 

class

 
SaleRepository

 
:

  !
IBaseRepository

" 1
<

1 2
Sale

2 6
>

6 7
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
SaleRepository !
(! "
AppDbContext" .
context/ 6
)6 7
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Sale& *
>* +
>+ ,
GetAllAsync- 8
(8 9
)9 :
{ 	
return 
await 
_context !
.! "
Sales" '
.' (
ToListAsync( 3
(3 4
)4 5
;5 6
} 	
public 
async 
Task 
< 
Sale 
? 
>  
GetByIdAsync! -
(- .
int. 1
id2 4
)4 5
{ 	
return 
await 
_context !
.! "
Sales" '
.' (
	FindAsync( 1
(1 2
id2 4
)4 5
;5 6
} 	
public 
async 
Task 
< 
Sale 
> 
AddAsync  (
(( )
Sale) -
entity. 4
)4 5
{ 	
await 
_context 
. 
Sales  
.  !
AddAsync! )
() *
entity* 0
)0 1
;1 2
await   
_context   
.   
SaveChangesAsync   +
(  + ,
)  , -
;  - .
return!! 
entity!! 
;!! 
}"" 	
public&& 
async&& 
Task&& 
<&& 
Sale&& 
?&& 
>&&  
UpdateAsync&&! ,
(&&, -
int&&- 0
id&&1 3
,&&3 4
Sale&&5 9
entity&&: @
)&&@ A
{'' 	
var(( 
sale(( 
=(( 
await(( 
_context(( %
.((% &
Sales((& +
.((+ ,
	FindAsync((, 5
(((5 6
id((6 8
)((8 9
;((9 :
if)) 
()) 
sale)) 
==)) 
null)) 
))) 
return)) $
null))% )
;))) *
sale++ 
.++ 

IdCustomer++ 
=++ 
entity++ $
.++$ %

IdCustomer++% /
;++/ 0
sale,, 
.,, 
IdSeller,, 
=,, 
entity,, "
.,," #
IdSeller,,# +
;,,+ ,
sale-- 
.-- 

TotalPrice-- 
=-- 
entity-- $
.--$ %

TotalPrice--% /
;--/ 0
sale.. 
... 
SaleDate.. 
=.. 
entity.. "
..." #
SaleDate..# +
;..+ ,
sale// 
.// 
SaleDetails// 
=// 
entity// %
.//% &
SaleDetails//& 1
;//1 2
await11 
_context11 
.11 
SaveChangesAsync11 +
(11+ ,
)11, -
;11- .
return22 
sale22 
;22 
}33 	
public55 
async55 
Task55 
<55 
bool55 
>55 
DeleteAsync55  +
(55+ ,
int55, /
id550 2
)552 3
{66 	
var77 
sale77 
=77 
await77 
_context77 %
.77% &
Sales77& +
.77+ ,
	FindAsync77, 5
(775 6
id776 8
)778 9
;779 :
if88 
(88 
sale88 
==88 
null88 
)88 
return88 $
false88% *
;88* +
_context:: 
.:: 
Sales:: 
.:: 
Remove:: !
(::! "
sale::" &
)::& '
;::' (
await;; 
_context;; 
.;; 
SaveChangesAsync;; +
(;;+ ,
);;, -
;;;- .
return<< 
true<< 
;<< 
}== 	
}AA 
}BB ¡*
qC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Repositories\Implementations\CustomerRepository.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Repositories  ,
., -
Implementations- <
{		 
public

 

class

 
CustomerRepository

 #
:

$ %
IBaseRepository

& 5
<

5 6
Customer

6 >
>

> ?
{ 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
CustomerRepository !
(! "
AppDbContext" .
context/ 6
)6 7
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Customer& .
>. /
>/ 0
GetAllAsync1 <
(< =
)= >
{ 	
return 
await 
_context !
.! "
	Customers" +
.+ ,
ToListAsync, 7
(7 8
)8 9
;9 :
} 	
public 
async 
Task 
< 
Customer "
?" #
># $
GetByIdAsync% 1
(1 2
int2 5
id6 8
)8 9
{ 	
return 
await 
_context !
.! "
	Customers" +
.+ ,
	FindAsync, 5
(5 6
id6 8
)8 9
;9 :
} 	
public 
async 
Task 
< 
Customer "
>" #
AddAsync$ ,
(, -
Customer- 5
entity6 <
)< =
{ 	
var   
newCustomer   
=   
new   !
Customer  " *
{!! 
Ci"" 
="" 
entity"" 
."" 
Ci"" 
,"" 
Name## 
=## 
entity## 
.## 
Name## "
,##" #
Email$$ 
=$$ 
entity$$ 
.$$ 
Email$$ $
,$$$ %
Phone%% 
=%% 
entity%% 
.%% 
Phone%% $
,%%$ %
Address&& 
=&& 
entity&&  
.&&  !
Address&&! (
}'' 
;'' 
_context)) 
.)) 
	Customers)) 
.)) 
Add)) "
())" #
newCustomer))# .
))). /
;))/ 0
await** 
_context** 
.** 
SaveChangesAsync** +
(**+ ,
)**, -
;**- .
return++ 
newCustomer++ 
;++ 
}-- 	
public// 
async// 
Task// 
<// 
Customer// "
?//" #
>//# $
UpdateAsync//% 0
(//0 1
int//1 4
id//5 7
,//7 8
Customer//9 A
entity//B H
)//H I
{00 	
var11 
customer11 
=11 
await11  
_context11! )
.11) *
	Customers11* 3
.113 4
	FindAsync114 =
(11= >
id11> @
)11@ A
;11A B
if22 
(22 
customer22 
==22 
null22  
)22  !
return22" (
null22) -
;22- .
customer44 
.44 
Ci44 
=44 
entity44  
.44  !
Ci44! #
;44# $
customer55 
.55 
Name55 
=55 
entity55 "
.55" #
Name55# '
;55' (
customer66 
.66 
Email66 
=66 
entity66 #
.66# $
Email66$ )
;66) *
customer77 
.77 
Phone77 
=77 
entity77 #
.77# $
Phone77$ )
;77) *
customer88 
.88 
Address88 
=88 
entity88 %
.88% &
Address88& -
;88- .
await:: 
_context:: 
.:: 
SaveChangesAsync:: +
(::+ ,
)::, -
;::- .
return;; 
customer;; 
;;; 
}<< 	
public>>	 
async>> 
Task>> 
<>> 
bool>> 
>>>  
DeleteAsync>>! ,
(>>, -
int>>- 0
id>>1 3
)>>3 4
{?? 	
var@@ 
customer@@ 
=@@ 
await@@  
_context@@! )
.@@) *
	Customers@@* 3
.@@3 4
	FindAsync@@4 =
(@@= >
id@@> @
)@@@ A
;@@A B
ifAA 
(AA 
customerAA 
==AA 
nullAA  
)AA  !
returnAA" (
falseAA) .
;AA. /
_contextCC 
.CC 
	CustomersCC 
.CC 
RemoveCC %
(CC% &
customerCC& .
)CC. /
;CC/ 0
awaitDD 
_contextDD 
.DD 
SaveChangesAsyncDD +
(DD+ ,
)DD, -
;DD- .
returnEE 
trueEE 
;EE 
}FF 	
}GG 
}HH Ø#
pC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Repositories\Implementations\ProductRepository.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Repositories  ,
., -
Implementations- <
{ 
public		 

class		 
ProductRepository		 "
:		# $
IBaseRepository		% 4
<		4 5
Product		5 <
>		< =
{

 
private 
readonly 
AppDbContext %
_context& .
;. /
public 
ProductRepository  
(  !
AppDbContext! -
context. 5
)5 6
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
Product& -
>- .
>. /
GetAllAsync0 ;
(; <
)< =
{ 	
return 
await 
_context !
.! "
Products" *
.* +
ToListAsync+ 6
(6 7
)7 8
;8 9
} 	
public 
async 
Task 
< 
Product !
?! "
>" #
GetByIdAsync$ 0
(0 1
int1 4
id5 7
)7 8
{ 	
return 
await 
_context !
.! "
Products" *
.* +
	FindAsync+ 4
(4 5
id5 7
)7 8
;8 9
} 	
public 
async 
Task 
< 
Product !
>! "
AddAsync# +
(+ ,
Product, 3
entity4 :
): ;
{ 	
await 
_context 
. 
Products #
.# $
AddAsync$ ,
(, -
entity- 3
)3 4
;4 5
await 
_context 
. 
SaveChangesAsync +
(+ ,
), -
;- .
return   
entity   
;   
}!! 	
public$$ 
async$$ 
Task$$ 
<$$ 
Product$$ !
?$$! "
>$$" #
UpdateAsync$$$ /
($$/ 0
int$$0 3
id$$4 6
,$$6 7
Product$$8 ?
entity$$@ F
)$$F G
{%% 	
var&& 
product&& 
=&& 
await&& 
_context&&  (
.&&( )
Products&&) 1
.&&1 2
	FindAsync&&2 ;
(&&; <
id&&< >
)&&> ?
;&&? @
if'' 
('' 
product'' 
=='' 
null'' 
)''  
return''! '
null''( ,
;'', -
product)) 
.)) 
Name)) 
=)) 
entity)) !
.))! "
Name))" &
;))& '
product** 
.** 
Price** 
=** 
entity** "
.**" #
Price**# (
;**( )
product++ 
.++ 
Description++ 
=++  !
entity++" (
.++( )
Description++) 4
;++4 5
await-- 
_context-- 
.-- 
SaveChangesAsync-- +
(--+ ,
)--, -
;--- .
return.. 
product.. 
;.. 
}// 	
public11 
async11 
Task11 
<11 
bool11 
>11 
DeleteAsync11  +
(11+ ,
int11, /
id110 2
)112 3
{22 	
var33 
product33 
=33 
await33 
_context33  (
.33( )
Products33) 1
.331 2
	FindAsync332 ;
(33; <
id33< >
)33> ?
;33? @
if44 
(44 
product44 
==44 
null44 
)44  
return44! '
false44( -
;44- .
_context66 
.66 
Products66 
.66 
Remove66 $
(66$ %
product66% ,
)66, -
;66- .
await77 
_context77 
.77 
SaveChangesAsync77 +
(77+ ,
)77, -
;77- .
return88 
true88 
;88 
}99 	
}:: 
};; ∑5
IC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. 
AddDbContext 
< 
AppDbContext *
>* +
(+ ,
options, 3
=>4 6
options 
. 
	UseNpgsql 
( 
builder 
. 
Configuration +
.+ ,
GetConnectionString, ?
(? @
$str@ S
)S T
)T U
)U V
;V W
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Services1 9
.9 :

Interfaces: D
.D E
ICustomerServiceE U
,U V!
LubricantesAyrthonAPIW l
.l m
Servicesm u
.u v
Implementations	v Ö
.
Ö Ü
CustomerService
Ü ï
>
ï ñ
(
ñ ó
)
ó ò
;
ò ô
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Services1 9
.9 :

Interfaces: D
.D E
ISellerServiceE S
,S T!
LubricantesAyrthonAPIU j
.j k
Servicesk s
.s t
Implementations	t É
.
É Ñ
SellerService
Ñ ë
>
ë í
(
í ì
)
ì î
;
î ï
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Services1 9
.9 :

Interfaces: D
.D E
ISaleServiceE Q
,Q R!
LubricantesAyrthonAPIS h
.h i
Servicesi q
.q r
Implementations	r Å
.
Å Ç
SaleService
Ç ç
>
ç é
(
é è
)
è ê
;
ê ë
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Services1 9
.9 :

Interfaces: D
.D E
IProductServiceE T
,T U!
LubricantesAyrthonAPIV k
.k l
Servicesl t
.t u
Implementations	u Ñ
.
Ñ Ö
ProductService
Ö ì
>
ì î
(
î ï
)
ï ñ
;
ñ ó
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Repositories1 =
.= >

Interfaces> H
.H I
IBaseRepositoryI X
<X Y!
LubricantesAyrthonAPIY n
.n o
Modelso u
.u v
Customerv ~
>~ 
,	 Ä#
LubricantesAyrthonAPI
Å ñ
.
ñ ó
Repositories
ó £
.
£ §
Implementations
§ ≥
.
≥ ¥ 
CustomerRepository
¥ ∆
>
∆ «
(
« »
)
» …
;
…  
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Repositories1 =
.= >

Interfaces> H
.H I
IBaseRepositoryI X
<X Y!
LubricantesAyrthonAPIY n
.n o
Modelso u
.u v
Sellerv |
>| }
,} ~"
LubricantesAyrthonAPI	 î
.
î ï
Repositories
ï °
.
° ¢
Implementations
¢ ±
.
± ≤
SellerRepository
≤ ¬
>
¬ √
(
√ ƒ
)
ƒ ≈
;
≈ ∆
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Repositories1 =
.= >

Interfaces> H
.H I
IBaseRepositoryI X
<X Y!
LubricantesAyrthonAPIY n
.n o
Modelso u
.u v
Salev z
>z {
,{ |"
LubricantesAyrthonAPI	} í
.
í ì
Repositories
ì ü
.
ü †
Implementations
† Ø
.
Ø ∞
SaleRepository
∞ æ
>
æ ø
(
ø ¿
)
¿ ¡
;
¡ ¬
builder 
. 
Services 
. 
	AddScoped 
< !
LubricantesAyrthonAPI 0
.0 1
Repositories1 =
.= >

Interfaces> H
.H I
IBaseRepositoryI X
<X Y!
LubricantesAyrthonAPIY n
.n o
Modelso u
.u v
Productv }
>} ~
,~ #
LubricantesAyrthonAPI
Ä ï
.
ï ñ
Repositories
ñ ¢
.
¢ £
Implementations
£ ≤
.
≤ ≥
ProductRepository
≥ ƒ
>
ƒ ≈
(
≈ ∆
)
∆ «
;
« »
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 
AddSwaggerGen 
( 
)  
;  !
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
if   
(   
app   
.   
Environment   
.   
IsDevelopment   !
(  ! "
)  " #
)  # $
{!! 
app"" 
."" 

UseSwagger"" 
("" 
)"" 
;"" 
app## 
.## 
UseSwaggerUI## 
(## 
)## 
;## 
}$$ 
app&& 
.&& 
UseHttpsRedirection&& 
(&& 
)&& 
;&& 
app'' 
.'' 
UseAuthorization'' 
('' 
)'' 
;'' 
app(( 
.(( 
MapControllers(( 
((( 
)(( 
;(( 
app)) 
.)) 
Run)) 
()) 
))) 	
;))	 
«
OC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Models\Seller.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Models  &
{ 
public 

class 
Seller 
{ 
[ 	
Key	 
] 
public		 
int		 
Id		 
{		 
get		 
;		 
set		  
;		  !
}		" #
[ 	
Required	 
] 
[ 	
	MinLength	 
( 
$num 
) 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
required 
string 
Ci !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
$num 
) 
] 
public 
required 
int 
Age 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
EmailAddress	 
( 
) 
] 
public 
string 
? 
Email 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Required	 
] 
[ 	
Phone	 
( 
) 
] 
public 
required 
string 
Phone $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 	
Required	 
] 
[   	
	MaxLength  	 
(   
$num   
)   
]   
public!! 
required!! 
string!! 
Address!! &
{!!' (
get!!) ,
;!!, -
set!!. 1
;!!1 2
}!!3 4
[## 	
Required##	 
]## 
[$$ 	
Range$$	 
($$ 
$num$$ 
,$$ 
double$$ 
.$$ 
MaxValue$$ #
)$$# $
]$$$ %
public%% 
decimal%% 
Salary%% 
{%% 
get%%  #
;%%# $
set%%% (
;%%( )
}%%* +
public'' 
List'' 
<'' 
Sale'' 
>'' 
?'' 
Sales''  
{''! "
get''# &
;''& '
set''( +
;''+ ,
}''- .
=''/ 0
new''1 4
(''4 5
)''5 6
;''6 7
}(( 
})) ∂
SC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Models\SaleDetail.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Models  &
{ 
public 

class 

SaleDetail 
{ 
[ 	
Key	 
] 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[

 	
Required

	 
]

 
public 
int 
IdSale 
{ 
get 
;  
set! $
;$ %
}& '
public 
Sale 
Sale 
{ 
get 
; 
set  #
;# $
}% &
=' (
null) -
!- .
;. /
[ 	
Required	 
] 
public 
required 
int 
	IdProduct %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
Product 
Product 
{  
get! $
;$ %
set& )
;) *
}+ ,
=- .
null/ 3
!3 4
;4 5
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
) 
]  
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
)$ %
]% &
public 
decimal 
	UnitPrice  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} ´
MC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Models\Sale.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Models  &
{ 
public 

class 
Sale 
{ 
[ 	
Key	 
] 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[

 	
Required

	 
]

 
public 
required 
int 

IdCustomer &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
Customer 
Customer  
{! "
get# &
;& '
set( +
;+ ,
}- .
=/ 0
null1 5
!5 6
;6 7
[ 	
Required	 
] 
public 
required 
int 
IdSeller $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
Seller 
Seller 
{ 
get "
;" #
set$ '
;' (
}) *
=+ ,
null- 1
!1 2
;2 3
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
)$ %
]% &
public 
required 
decimal 

TotalPrice  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
[ 	
Required	 
] 
[ 	
DataType	 
( 
DataType 
. 
Date 
)  
]  !
public 
DateTime 
SaleDate  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Required	 
] 
public 
List 
< 

SaleDetail 
> 
?  
SaleDetails! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
=; <
new= @
(@ A
)A B
;B C
} 
} ˇ
PC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Models\Product.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Models  &
{ 
public 

class 
Product 
{ 
[ 	
Key	 
] 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[

 	
Required

	 
]

 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
)$ %
]% &
public 
required 
decimal 
Price  %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
) 
]  
public 
required 
int 
Stock !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
List 
< 

SaleDetail 
> 
?  
SaleDetails! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
=; <
new= @
(@ A
)A B
;B C
} 
} ˝
QC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Models\Customer.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Models  &
{ 
public 

class 
Customer 
{ 
[ 	
Key	 
] 
public		 
int		 
Id		 
{		 
get		 
;		 
set		  
;		  !
}		" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
[ 	
	MinLength	 
( 
$num 
) 
] 
public 
required 
string 
Ci !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
EmailAddress	 
( 
) 
] 
public 
string 
? 
Email 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Phone	 
( 
) 
] 
public 
string 
? 
Phone 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
List 
< 
Sale 
> 
? 
Sales  
{! "
get# &
;& '
set( +
;+ ,
}- .
=/ 0
new1 4
(4 5
)5 6
;6 7
} 
} µÌ
pC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Migrations\20250914204716_AddUniqueConstraints.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  

Migrations  *
{ 
public		 

partial		 
class		  
AddUniqueConstraints		 -
:		. /
	Migration		0 9
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
DropForeignKey +
(+ ,
name 
: 
$str 2
,2 3
table 
: 
$str #
)# $
;$ %
migrationBuilder 
. 
DropPrimaryKey +
(+ ,
name 
: 
$str %
,% &
table 
: 
$str #
)# $
;$ %
migrationBuilder 
. 
	DropIndex &
(& '
name 
: 
$str ,
,, -
table 
: 
$str #
)# $
;$ %
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str 
, 
table 
: 
$str #
)# $
;$ %
migrationBuilder 
. 
RenameTable (
(( )
name 
: 
$str "
," #
newName   
:   
$str   &
)  & '
;  ' (
migrationBuilder"" 
."" 
RenameColumn"" )
("") *
name## 
:## 
$str## "
,##" #
table$$ 
:$$ 
$str$$ 
,$$ 
newName%% 
:%% 
$str%% %
)%%% &
;%%& '
migrationBuilder'' 
.'' 
RenameColumn'' )
('') *
name(( 
:(( 
$str((  
,((  !
table)) 
:)) 
$str)) 
,)) 
newName** 
:** 
$str** #
)**# $
;**$ %
migrationBuilder,, 
.,, 
RenameColumn,, )
(,,) *
name-- 
:-- 
$str--  
,--  !
table.. 
:.. 
$str.. 
,.. 
newName// 
:// 
$str// #
)//# $
;//$ %
migrationBuilder11 
.11 
RenameColumn11 )
(11) *
name22 
:22 
$str22 "
,22" #
table33 
:33 
$str33 
,33 
newName44 
:44 
$str44 %
)44% &
;44& '
migrationBuilder66 
.66 
RenameColumn66 )
(66) *
name77 
:77 
$str77 
,77 
table88 
:88 
$str88 
,88 
newName99 
:99 
$str99 
)99 
;99 
migrationBuilder;; 
.;; 
RenameColumn;; )
(;;) *
name<< 
:<< 
$str<< 
,<< 
table== 
:== 
$str== !
,==! "
newName>> 
:>> 
$str>>  
)>>  !
;>>! "
migrationBuilder@@ 
.@@ 
RenameColumn@@ )
(@@) *
nameAA 
:AA 
$strAA 
,AA 
tableBB 
:BB 
$strBB !
,BB! "
newNameCC 
:CC 
$strCC  
)CC  !
;CC! "
migrationBuilderEE 
.EE 
RenameColumnEE )
(EE) *
nameFF 
:FF 
$strFF 
,FF 
tableGG 
:GG 
$strGG !
,GG! "
newNameHH 
:HH 
$strHH 
)HH  
;HH  !
migrationBuilderJJ 
.JJ 
RenameColumnJJ )
(JJ) *
nameKK 
:KK 
$strKK #
,KK# $
tableLL 
:LL 
$strLL !
,LL! "
newNameMM 
:MM 
$strMM &
)MM& '
;MM' (
migrationBuilderOO 
.OO 
RenameColumnOO )
(OO) *
namePP 
:PP 
$strPP 
,PP 
tableQQ 
:QQ 
$strQQ !
,QQ! "
newNameRR 
:RR 
$strRR 
)RR 
;RR 
migrationBuilderTT 
.TT 
RenameColumnTT )
(TT) *
nameUU 
:UU 
$strUU  
,UU  !
tableVV 
:VV 
$strVV $
,VV$ %
newNameWW 
:WW 
$strWW #
)WW# $
;WW$ %
migrationBuilderYY 
.YY 
RenameColumnYY )
(YY) *
nameZZ 
:ZZ 
$strZZ 
,ZZ 
table[[ 
:[[ 
$str[[ $
,[[$ %
newName\\ 
:\\ 
$str\\  
)\\  !
;\\! "
migrationBuilder^^ 
.^^ 
RenameColumn^^ )
(^^) *
name__ 
:__ 
$str__ 
,__ 
table`` 
:`` 
$str`` $
,``$ %
newNameaa 
:aa 
$straa !
)aa! "
;aa" #
migrationBuildercc 
.cc 
RenameColumncc )
(cc) *
namedd 
:dd 
$strdd !
,dd! "
tableee 
:ee 
$stree $
,ee$ %
newNameff 
:ff 
$strff $
)ff$ %
;ff% &
migrationBuilderhh 
.hh 
RenameColumnhh )
(hh) *
nameii 
:ii 
$strii 
,ii 
tablejj 
:jj 
$strjj $
,jj$ %
newNamekk 
:kk 
$strkk 
)kk 
;kk 
migrationBuildermm 
.mm 
AlterColumnmm (
<mm( )
stringmm) /
>mm/ 0
(mm0 1
namenn 
:nn 
$strnn 
,nn 
tableoo 
:oo 
$stroo !
,oo! "
typepp 
:pp 
$strpp .
,pp. /
	maxLengthqq 
:qq 
$numqq 
,qq 
nullablerr 
:rr 
falserr 
,rr  

oldClrTypess 
:ss 
typeofss "
(ss" #
stringss# )
)ss) *
,ss* +
oldTypett 
:tt 
$strtt 
)tt  
;tt  !
migrationBuildervv 
.vv 
AlterColumnvv (
<vv( )
stringvv) /
>vv/ 0
(vv0 1
nameww 
:ww 
$strww #
,ww# $
tablexx 
:xx 
$strxx !
,xx! "
typeyy 
:yy 
$stryy .
,yy. /
	maxLengthzz 
:zz 
$numzz 
,zz 
nullable{{ 
:{{ 
true{{ 
,{{ 

oldClrType|| 
:|| 
typeof|| "
(||" #
string||# )
)||) *
,||* +
oldType}} 
:}} 
$str}} 
,}}  
oldNullable~~ 
:~~ 
true~~ !
)~~! "
;~~" #
migrationBuilder
ÄÄ 
.
ÄÄ 
AlterColumn
ÄÄ (
<
ÄÄ( )
string
ÄÄ) /
>
ÄÄ/ 0
(
ÄÄ0 1
name
ÅÅ 
:
ÅÅ 
$str
ÅÅ 
,
ÅÅ  
table
ÇÇ 
:
ÇÇ 
$str
ÇÇ "
,
ÇÇ" #
type
ÉÉ 
:
ÉÉ 
$str
ÉÉ .
,
ÉÉ. /
	maxLength
ÑÑ 
:
ÑÑ 
$num
ÑÑ 
,
ÑÑ 
nullable
ÖÖ 
:
ÖÖ 
true
ÖÖ 
,
ÖÖ 

oldClrType
ÜÜ 
:
ÜÜ 
typeof
ÜÜ "
(
ÜÜ" #
string
ÜÜ# )
)
ÜÜ) *
,
ÜÜ* +
oldType
áá 
:
áá 
$str
áá 
,
áá  
oldNullable
àà 
:
àà 
true
àà !
)
àà! "
;
àà" #
migrationBuilder
ää 
.
ää 
	AddColumn
ää &
<
ää& '
string
ää' -
>
ää- .
(
ää. /
name
ãã 
:
ãã 
$str
ãã 
,
ãã 
table
åå 
:
åå 
$str
åå "
,
åå" #
type
çç 
:
çç 
$str
çç -
,
çç- .
	maxLength
éé 
:
éé 
$num
éé 
,
éé 
nullable
èè 
:
èè 
false
èè 
,
èè  
defaultValue
êê 
:
êê 
$str
êê  
)
êê  !
;
êê! "
migrationBuilder
íí 
.
íí 
AddPrimaryKey
íí *
(
íí* +
name
ìì 
:
ìì 
$str
ìì &
,
ìì& '
table
îî 
:
îî 
$str
îî $
,
îî$ %
column
ïï 
:
ïï 
$str
ïï 
)
ïï 
;
ïï 
migrationBuilder
óó 
.
óó 
CreateTable
óó (
(
óó( )
name
òò 
:
òò 
$str
òò 
,
òò  
columns
ôô 
:
ôô 
table
ôô 
=>
ôô !
new
ôô" %
{
öö 
Id
õõ 
=
õõ 
table
õõ 
.
õõ 
Column
õõ %
<
õõ% &
int
õõ& )
>
õõ) *
(
õõ* +
type
õõ+ /
:
õõ/ 0
$str
õõ1 :
,
õõ: ;
nullable
õõ< D
:
õõD E
false
õõF K
)
õõK L
.
úú 

Annotation
úú #
(
úú# $
$str
úú$ D
,
úúD E+
NpgsqlValueGenerationStrategy
úúF c
.
úúc d%
IdentityByDefaultColumn
úúd {
)
úú{ |
,
úú| }
Ci
ùù 
=
ùù 
table
ùù 
.
ùù 
Column
ùù %
<
ùù% &
string
ùù& ,
>
ùù, -
(
ùù- .
type
ùù. 2
:
ùù2 3
$str
ùù4 K
,
ùùK L
	maxLength
ùùM V
:
ùùV W
$num
ùùX Z
,
ùùZ [
nullable
ùù\ d
:
ùùd e
false
ùùf k
)
ùùk l
,
ùùl m
Name
ûû 
=
ûû 
table
ûû  
.
ûû  !
Column
ûû! '
<
ûû' (
string
ûû( .
>
ûû. /
(
ûû/ 0
type
ûû0 4
:
ûû4 5
$str
ûû6 N
,
ûûN O
	maxLength
ûûP Y
:
ûûY Z
$num
ûû[ ^
,
ûû^ _
nullable
ûû` h
:
ûûh i
false
ûûj o
)
ûûo p
,
ûûp q
Age
üü 
=
üü 
table
üü 
.
üü  
Column
üü  &
<
üü& '
int
üü' *
>
üü* +
(
üü+ ,
type
üü, 0
:
üü0 1
$str
üü2 ;
,
üü; <
nullable
üü= E
:
üüE F
false
üüG L
)
üüL M
,
üüM N
Email
†† 
=
†† 
table
†† !
.
††! "
Column
††" (
<
††( )
string
††) /
>
††/ 0
(
††0 1
type
††1 5
:
††5 6
$str
††7 =
,
††= >
nullable
††? G
:
††G H
true
††I M
)
††M N
,
††N O
Phone
°° 
=
°° 
table
°° !
.
°°! "
Column
°°" (
<
°°( )
string
°°) /
>
°°/ 0
(
°°0 1
type
°°1 5
:
°°5 6
$str
°°7 =
,
°°= >
nullable
°°? G
:
°°G H
false
°°I N
)
°°N O
,
°°O P
Address
¢¢ 
=
¢¢ 
table
¢¢ #
.
¢¢# $
Column
¢¢$ *
<
¢¢* +
string
¢¢+ 1
>
¢¢1 2
(
¢¢2 3
type
¢¢3 7
:
¢¢7 8
$str
¢¢9 Q
,
¢¢Q R
	maxLength
¢¢S \
:
¢¢\ ]
$num
¢¢^ a
,
¢¢a b
nullable
¢¢c k
:
¢¢k l
false
¢¢m r
)
¢¢r s
,
¢¢s t
Salary
££ 
=
££ 
table
££ "
.
££" #
Column
££# )
<
££) *
decimal
££* 1
>
££1 2
(
££2 3
type
££3 7
:
££7 8
$str
££9 B
,
££B C
nullable
££D L
:
££L M
false
££N S
)
££S T
}
§§ 
,
§§ 
constraints
•• 
:
•• 
table
•• "
=>
••# %
{
¶¶ 
table
ßß 
.
ßß 

PrimaryKey
ßß $
(
ßß$ %
$str
ßß% 1
,
ßß1 2
x
ßß3 4
=>
ßß5 7
x
ßß8 9
.
ßß9 :
Id
ßß: <
)
ßß< =
;
ßß= >
}
®® 
)
®® 
;
®® 
migrationBuilder
™™ 
.
™™ 
CreateIndex
™™ (
(
™™( )
name
´´ 
:
´´ 
$str
´´ +
,
´´+ ,
table
¨¨ 
:
¨¨ 
$str
¨¨ 
,
¨¨ 
column
≠≠ 
:
≠≠ 
$str
≠≠ $
)
≠≠$ %
;
≠≠% &
migrationBuilder
ØØ 
.
ØØ 
CreateIndex
ØØ (
(
ØØ( )
name
∞∞ 
:
∞∞ 
$str
∞∞ )
,
∞∞) *
table
±± 
:
±± 
$str
±± 
,
±± 
column
≤≤ 
:
≤≤ 
$str
≤≤ "
)
≤≤" #
;
≤≤# $
migrationBuilder
¥¥ 
.
¥¥ 
CreateIndex
¥¥ (
(
¥¥( )
name
µµ 
:
µµ 
$str
µµ '
,
µµ' (
table
∂∂ 
:
∂∂ 
$str
∂∂ "
,
∂∂" #
column
∑∑ 
:
∑∑ 
$str
∑∑ 
,
∑∑ 
unique
∏∏ 
:
∏∏ 
true
∏∏ 
)
∏∏ 
;
∏∏ 
migrationBuilder
∫∫ 
.
∫∫ 
CreateIndex
∫∫ (
(
∫∫( )
name
ªª 
:
ªª 
$str
ªª 0
,
ªª0 1
table
ºº 
:
ºº 
$str
ºº $
,
ºº$ %
column
ΩΩ 
:
ΩΩ 
$str
ΩΩ #
)
ΩΩ# $
;
ΩΩ$ %
migrationBuilder
øø 
.
øø 
CreateIndex
øø (
(
øø( )
name
¿¿ 
:
¿¿ 
$str
¿¿ -
,
¿¿- .
table
¡¡ 
:
¡¡ 
$str
¡¡ $
,
¡¡$ %
column
¬¬ 
:
¬¬ 
$str
¬¬  
)
¬¬  !
;
¬¬! "
migrationBuilder
ƒƒ 
.
ƒƒ 
AddForeignKey
ƒƒ *
(
ƒƒ* +
name
≈≈ 
:
≈≈ 
$str
≈≈ 9
,
≈≈9 :
table
∆∆ 
:
∆∆ 
$str
∆∆ $
,
∆∆$ %
column
«« 
:
«« 
$str
«« #
,
««# $
principalTable
»» 
:
»» 
$str
»»  *
,
»»* +
principalColumn
…… 
:
……  
$str
……! %
,
……% &
onDelete
   
:
   
ReferentialAction
   +
.
  + ,
Restrict
  , 4
)
  4 5
;
  5 6
migrationBuilder
ÃÃ 
.
ÃÃ 
AddForeignKey
ÃÃ *
(
ÃÃ* +
name
ÕÕ 
:
ÕÕ 
$str
ÕÕ 3
,
ÕÕ3 4
table
ŒŒ 
:
ŒŒ 
$str
ŒŒ $
,
ŒŒ$ %
column
œœ 
:
œœ 
$str
œœ  
,
œœ  !
principalTable
–– 
:
–– 
$str
––  '
,
––' (
principalColumn
—— 
:
——  
$str
——! %
,
——% &
onDelete
““ 
:
““ 
ReferentialAction
““ +
.
““+ ,
Cascade
““, 3
)
““3 4
;
““4 5
migrationBuilder
‘‘ 
.
‘‘ 
AddForeignKey
‘‘ *
(
‘‘* +
name
’’ 
:
’’ 
$str
’’ 5
,
’’5 6
table
÷÷ 
:
÷÷ 
$str
÷÷ 
,
÷÷ 
column
◊◊ 
:
◊◊ 
$str
◊◊ $
,
◊◊$ %
principalTable
ÿÿ 
:
ÿÿ 
$str
ÿÿ  +
,
ÿÿ+ ,
principalColumn
ŸŸ 
:
ŸŸ  
$str
ŸŸ! %
,
ŸŸ% &
onDelete
⁄⁄ 
:
⁄⁄ 
ReferentialAction
⁄⁄ +
.
⁄⁄+ ,
Restrict
⁄⁄, 4
)
⁄⁄4 5
;
⁄⁄5 6
migrationBuilder
‹‹ 
.
‹‹ 
AddForeignKey
‹‹ *
(
‹‹* +
name
›› 
:
›› 
$str
›› 1
,
››1 2
table
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ 
,
ﬁﬁ 
column
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ "
,
ﬂﬂ" #
principalTable
‡‡ 
:
‡‡ 
$str
‡‡  )
,
‡‡) *
principalColumn
·· 
:
··  
$str
··! %
,
··% &
onDelete
‚‚ 
:
‚‚ 
ReferentialAction
‚‚ +
.
‚‚+ ,
Restrict
‚‚, 4
)
‚‚4 5
;
‚‚5 6
}
„„ 	
	protected
ÊÊ 
override
ÊÊ 
void
ÊÊ 
Down
ÊÊ  $
(
ÊÊ$ %
MigrationBuilder
ÊÊ% 5
migrationBuilder
ÊÊ6 F
)
ÊÊF G
{
ÁÁ 	
migrationBuilder
ËË 
.
ËË 
DropForeignKey
ËË +
(
ËË+ ,
name
ÈÈ 
:
ÈÈ 
$str
ÈÈ 9
,
ÈÈ9 :
table
ÍÍ 
:
ÍÍ 
$str
ÍÍ $
)
ÍÍ$ %
;
ÍÍ% &
migrationBuilder
ÏÏ 
.
ÏÏ 
DropForeignKey
ÏÏ +
(
ÏÏ+ ,
name
ÌÌ 
:
ÌÌ 
$str
ÌÌ 3
,
ÌÌ3 4
table
ÓÓ 
:
ÓÓ 
$str
ÓÓ $
)
ÓÓ$ %
;
ÓÓ% &
migrationBuilder
 
.
 
DropForeignKey
 +
(
+ ,
name
ÒÒ 
:
ÒÒ 
$str
ÒÒ 5
,
ÒÒ5 6
table
ÚÚ 
:
ÚÚ 
$str
ÚÚ 
)
ÚÚ 
;
ÚÚ  
migrationBuilder
ÙÙ 
.
ÙÙ 
DropForeignKey
ÙÙ +
(
ÙÙ+ ,
name
ıı 
:
ıı 
$str
ıı 1
,
ıı1 2
table
ˆˆ 
:
ˆˆ 
$str
ˆˆ 
)
ˆˆ 
;
ˆˆ  
migrationBuilder
¯¯ 
.
¯¯ 
	DropTable
¯¯ &
(
¯¯& '
name
˘˘ 
:
˘˘ 
$str
˘˘ 
)
˘˘  
;
˘˘  !
migrationBuilder
˚˚ 
.
˚˚ 
	DropIndex
˚˚ &
(
˚˚& '
name
¸¸ 
:
¸¸ 
$str
¸¸ +
,
¸¸+ ,
table
˝˝ 
:
˝˝ 
$str
˝˝ 
)
˝˝ 
;
˝˝  
migrationBuilder
ˇˇ 
.
ˇˇ 
	DropIndex
ˇˇ &
(
ˇˇ& '
name
ÄÄ 
:
ÄÄ 
$str
ÄÄ )
,
ÄÄ) *
table
ÅÅ 
:
ÅÅ 
$str
ÅÅ 
)
ÅÅ 
;
ÅÅ  
migrationBuilder
ÉÉ 
.
ÉÉ 
	DropIndex
ÉÉ &
(
ÉÉ& '
name
ÑÑ 
:
ÑÑ 
$str
ÑÑ '
,
ÑÑ' (
table
ÖÖ 
:
ÖÖ 
$str
ÖÖ "
)
ÖÖ" #
;
ÖÖ# $
migrationBuilder
áá 
.
áá 
DropPrimaryKey
áá +
(
áá+ ,
name
àà 
:
àà 
$str
àà &
,
àà& '
table
ââ 
:
ââ 
$str
ââ $
)
ââ$ %
;
ââ% &
migrationBuilder
ãã 
.
ãã 
	DropIndex
ãã &
(
ãã& '
name
åå 
:
åå 
$str
åå 0
,
åå0 1
table
çç 
:
çç 
$str
çç $
)
çç$ %
;
çç% &
migrationBuilder
èè 
.
èè 
	DropIndex
èè &
(
èè& '
name
êê 
:
êê 
$str
êê -
,
êê- .
table
ëë 
:
ëë 
$str
ëë $
)
ëë$ %
;
ëë% &
migrationBuilder
ìì 
.
ìì 

DropColumn
ìì '
(
ìì' (
name
îî 
:
îî 
$str
îî 
,
îî 
table
ïï 
:
ïï 
$str
ïï "
)
ïï" #
;
ïï# $
migrationBuilder
óó 
.
óó 
RenameTable
óó (
(
óó( )
name
òò 
:
òò 
$str
òò #
,
òò# $
newName
ôô 
:
ôô 
$str
ôô %
)
ôô% &
;
ôô& '
migrationBuilder
õõ 
.
õõ 
RenameColumn
õõ )
(
õõ) *
name
úú 
:
úú 
$str
úú "
,
úú" #
table
ùù 
:
ùù 
$str
ùù 
,
ùù 
newName
ûû 
:
ûû 
$str
ûû %
)
ûû% &
;
ûû& '
migrationBuilder
†† 
.
†† 
RenameColumn
†† )
(
††) *
name
°° 
:
°° 
$str
°°  
,
°°  !
table
¢¢ 
:
¢¢ 
$str
¢¢ 
,
¢¢ 
newName
££ 
:
££ 
$str
££ #
)
££# $
;
££$ %
migrationBuilder
•• 
.
•• 
RenameColumn
•• )
(
••) *
name
¶¶ 
:
¶¶ 
$str
¶¶  
,
¶¶  !
table
ßß 
:
ßß 
$str
ßß 
,
ßß 
newName
®® 
:
®® 
$str
®® #
)
®®# $
;
®®$ %
migrationBuilder
™™ 
.
™™ 
RenameColumn
™™ )
(
™™) *
name
´´ 
:
´´ 
$str
´´ "
,
´´" #
table
¨¨ 
:
¨¨ 
$str
¨¨ 
,
¨¨ 
newName
≠≠ 
:
≠≠ 
$str
≠≠ %
)
≠≠% &
;
≠≠& '
migrationBuilder
ØØ 
.
ØØ 
RenameColumn
ØØ )
(
ØØ) *
name
∞∞ 
:
∞∞ 
$str
∞∞ 
,
∞∞ 
table
±± 
:
±± 
$str
±± 
,
±± 
newName
≤≤ 
:
≤≤ 
$str
≤≤ 
)
≤≤ 
;
≤≤ 
migrationBuilder
¥¥ 
.
¥¥ 
RenameColumn
¥¥ )
(
¥¥) *
name
µµ 
:
µµ 
$str
µµ 
,
µµ 
table
∂∂ 
:
∂∂ 
$str
∂∂ !
,
∂∂! "
newName
∑∑ 
:
∑∑ 
$str
∑∑  
)
∑∑  !
;
∑∑! "
migrationBuilder
ππ 
.
ππ 
RenameColumn
ππ )
(
ππ) *
name
∫∫ 
:
∫∫ 
$str
∫∫ 
,
∫∫ 
table
ªª 
:
ªª 
$str
ªª !
,
ªª! "
newName
ºº 
:
ºº 
$str
ºº  
)
ºº  !
;
ºº! "
migrationBuilder
ææ 
.
ææ 
RenameColumn
ææ )
(
ææ) *
name
øø 
:
øø 
$str
øø 
,
øø 
table
¿¿ 
:
¿¿ 
$str
¿¿ !
,
¿¿! "
newName
¡¡ 
:
¡¡ 
$str
¡¡ 
)
¡¡  
;
¡¡  !
migrationBuilder
√√ 
.
√√ 
RenameColumn
√√ )
(
√√) *
name
ƒƒ 
:
ƒƒ 
$str
ƒƒ #
,
ƒƒ# $
table
≈≈ 
:
≈≈ 
$str
≈≈ !
,
≈≈! "
newName
∆∆ 
:
∆∆ 
$str
∆∆ &
)
∆∆& '
;
∆∆' (
migrationBuilder
»» 
.
»» 
RenameColumn
»» )
(
»») *
name
…… 
:
…… 
$str
…… 
,
…… 
table
   
:
   
$str
   !
,
  ! "
newName
ÀÀ 
:
ÀÀ 
$str
ÀÀ 
)
ÀÀ 
;
ÀÀ 
migrationBuilder
ÕÕ 
.
ÕÕ 
RenameColumn
ÕÕ )
(
ÕÕ) *
name
ŒŒ 
:
ŒŒ 
$str
ŒŒ  
,
ŒŒ  !
table
œœ 
:
œœ 
$str
œœ #
,
œœ# $
newName
–– 
:
–– 
$str
–– #
)
––# $
;
––$ %
migrationBuilder
““ 
.
““ 
RenameColumn
““ )
(
““) *
name
”” 
:
”” 
$str
”” 
,
”” 
table
‘‘ 
:
‘‘ 
$str
‘‘ #
,
‘‘# $
newName
’’ 
:
’’ 
$str
’’  
)
’’  !
;
’’! "
migrationBuilder
◊◊ 
.
◊◊ 
RenameColumn
◊◊ )
(
◊◊) *
name
ÿÿ 
:
ÿÿ 
$str
ÿÿ 
,
ÿÿ 
table
ŸŸ 
:
ŸŸ 
$str
ŸŸ #
,
ŸŸ# $
newName
⁄⁄ 
:
⁄⁄ 
$str
⁄⁄ !
)
⁄⁄! "
;
⁄⁄" #
migrationBuilder
‹‹ 
.
‹‹ 
RenameColumn
‹‹ )
(
‹‹) *
name
›› 
:
›› 
$str
›› !
,
››! "
table
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ #
,
ﬁﬁ# $
newName
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ $
)
ﬂﬂ$ %
;
ﬂﬂ% &
migrationBuilder
·· 
.
·· 
RenameColumn
·· )
(
··) *
name
‚‚ 
:
‚‚ 
$str
‚‚ 
,
‚‚ 
table
„„ 
:
„„ 
$str
„„ #
,
„„# $
newName
‰‰ 
:
‰‰ 
$str
‰‰ 
)
‰‰ 
;
‰‰ 
migrationBuilder
ÊÊ 
.
ÊÊ 
AlterColumn
ÊÊ (
<
ÊÊ( )
string
ÊÊ) /
>
ÊÊ/ 0
(
ÊÊ0 1
name
ÁÁ 
:
ÁÁ 
$str
ÁÁ 
,
ÁÁ 
table
ËË 
:
ËË 
$str
ËË !
,
ËË! "
type
ÈÈ 
:
ÈÈ 
$str
ÈÈ 
,
ÈÈ 
nullable
ÍÍ 
:
ÍÍ 
false
ÍÍ 
,
ÍÍ  

oldClrType
ÎÎ 
:
ÎÎ 
typeof
ÎÎ "
(
ÎÎ" #
string
ÎÎ# )
)
ÎÎ) *
,
ÎÎ* +
oldType
ÏÏ 
:
ÏÏ 
$str
ÏÏ 1
,
ÏÏ1 2
oldMaxLength
ÌÌ 
:
ÌÌ 
$num
ÌÌ !
)
ÌÌ! "
;
ÌÌ" #
migrationBuilder
ÔÔ 
.
ÔÔ 
AlterColumn
ÔÔ (
<
ÔÔ( )
string
ÔÔ) /
>
ÔÔ/ 0
(
ÔÔ0 1
name
 
:
 
$str
 #
,
# $
table
ÒÒ 
:
ÒÒ 
$str
ÒÒ !
,
ÒÒ! "
type
ÚÚ 
:
ÚÚ 
$str
ÚÚ 
,
ÚÚ 
nullable
ÛÛ 
:
ÛÛ 
true
ÛÛ 
,
ÛÛ 

oldClrType
ÙÙ 
:
ÙÙ 
typeof
ÙÙ "
(
ÙÙ" #
string
ÙÙ# )
)
ÙÙ) *
,
ÙÙ* +
oldType
ıı 
:
ıı 
$str
ıı 1
,
ıı1 2
oldMaxLength
ˆˆ 
:
ˆˆ 
$num
ˆˆ !
,
ˆˆ! "
oldNullable
˜˜ 
:
˜˜ 
true
˜˜ !
)
˜˜! "
;
˜˜" #
migrationBuilder
˘˘ 
.
˘˘ 
AlterColumn
˘˘ (
<
˘˘( )
string
˘˘) /
>
˘˘/ 0
(
˘˘0 1
name
˙˙ 
:
˙˙ 
$str
˙˙ 
,
˙˙  
table
˚˚ 
:
˚˚ 
$str
˚˚ "
,
˚˚" #
type
¸¸ 
:
¸¸ 
$str
¸¸ 
,
¸¸ 
nullable
˝˝ 
:
˝˝ 
true
˝˝ 
,
˝˝ 

oldClrType
˛˛ 
:
˛˛ 
typeof
˛˛ "
(
˛˛" #
string
˛˛# )
)
˛˛) *
,
˛˛* +
oldType
ˇˇ 
:
ˇˇ 
$str
ˇˇ 1
,
ˇˇ1 2
oldMaxLength
ÄÄ 
:
ÄÄ 
$num
ÄÄ !
,
ÄÄ! "
oldNullable
ÅÅ 
:
ÅÅ 
true
ÅÅ !
)
ÅÅ! "
;
ÅÅ" #
migrationBuilder
ÉÉ 
.
ÉÉ 
	AddColumn
ÉÉ &
<
ÉÉ& '
int
ÉÉ' *
>
ÉÉ* +
(
ÉÉ+ ,
name
ÑÑ 
:
ÑÑ 
$str
ÑÑ 
,
ÑÑ 
table
ÖÖ 
:
ÖÖ 
$str
ÖÖ #
,
ÖÖ# $
type
ÜÜ 
:
ÜÜ 
$str
ÜÜ 
,
ÜÜ  
nullable
áá 
:
áá 
true
áá 
)
áá 
;
áá  
migrationBuilder
ââ 
.
ââ 
AddPrimaryKey
ââ *
(
ââ* +
name
ää 
:
ää 
$str
ää %
,
ää% &
table
ãã 
:
ãã 
$str
ãã #
,
ãã# $
column
åå 
:
åå 
$str
åå 
)
åå 
;
åå 
migrationBuilder
éé 
.
éé 
CreateIndex
éé (
(
éé( )
name
èè 
:
èè 
$str
èè ,
,
èè, -
table
êê 
:
êê 
$str
êê #
,
êê# $
column
ëë 
:
ëë 
$str
ëë  
)
ëë  !
;
ëë! "
migrationBuilder
ìì 
.
ìì 
AddForeignKey
ìì *
(
ìì* +
name
îî 
:
îî 
$str
îî 2
,
îî2 3
table
ïï 
:
ïï 
$str
ïï #
,
ïï# $
column
ññ 
:
ññ 
$str
ññ  
,
ññ  !
principalTable
óó 
:
óó 
$str
óó  '
,
óó' (
principalColumn
òò 
:
òò  
$str
òò! %
)
òò% &
;
òò& '
}
ôô 	
}
öö 
}õõ ¯\
iC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Migrations\20250909012714_InitialCreate.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  

Migrations  *
{ 
public

 

partial

 
class

 
InitialCreate

 &
:

' (
	Migration

) 2
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str !
,! "
columns 
: 
table 
=> !
new" %
{ 
Id 
= 
table 
. 
Column %
<% &
int& )
>) *
(* +
type+ /
:/ 0
$str1 :
,: ;
nullable< D
:D E
falseF K
)K L
. 

Annotation #
(# $
$str$ D
,D E)
NpgsqlValueGenerationStrategyF c
.c d#
IdentityByDefaultColumnd {
){ |
,| }
Name 
= 
table  
.  !
Column! '
<' (
string( .
>. /
(/ 0
type0 4
:4 5
$str6 N
,N O
	maxLengthP Y
:Y Z
$num[ ^
,^ _
nullable` h
:h i
falsej o
)o p
,p q
Email 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 =
,= >
nullable? G
:G H
trueI M
)M N
,N O
Phone 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 =
,= >
nullable? G
:G H
trueI M
)M N
,N O
Address 
= 
table #
.# $
Column$ *
<* +
string+ 1
>1 2
(2 3
type3 7
:7 8
$str9 ?
,? @
nullableA I
:I J
trueK O
)O P
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% 3
,3 4
x5 6
=>7 9
x: ;
.; <
Id< >
)> ?
;? @
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name   
:   
$str    
,    !
columns!! 
:!! 
table!! 
=>!! !
new!!" %
{"" 
id## 
=## 
table## 
.## 
Column## %
<##% &
int##& )
>##) *
(##* +
type##+ /
:##/ 0
$str##1 :
,##: ;
nullable##< D
:##D E
false##F K
)##K L
.$$ 

Annotation$$ #
($$# $
$str$$$ D
,$$D E)
NpgsqlValueGenerationStrategy$$F c
.$$c d#
IdentityByDefaultColumn$$d {
)$${ |
,$$| }
name%% 
=%% 
table%%  
.%%  !
Column%%! '
<%%' (
string%%( .
>%%. /
(%%/ 0
type%%0 4
:%%4 5
$str%%6 <
,%%< =
nullable%%> F
:%%F G
false%%H M
)%%M N
,%%N O
description&& 
=&&  !
table&&" '
.&&' (
Column&&( .
<&&. /
string&&/ 5
>&&5 6
(&&6 7
type&&7 ;
:&&; <
$str&&= C
,&&C D
nullable&&E M
:&&M N
true&&O S
)&&S T
,&&T U
price'' 
='' 
table'' !
.''! "
Column''" (
<''( )
decimal'') 0
>''0 1
(''1 2
type''2 6
:''6 7
$str''8 A
,''A B
nullable''C K
:''K L
false''M R
)''R S
,''S T
stock(( 
=(( 
table(( !
.((! "
Column((" (
<((( )
int(() ,
>((, -
(((- .
type((. 2
:((2 3
$str((4 =
,((= >
nullable((? G
:((G H
false((I N
)((N O
})) 
,)) 
constraints** 
:** 
table** "
=>**# %
{++ 
table,, 
.,, 

PrimaryKey,, $
(,,$ %
$str,,% 2
,,,2 3
x,,4 5
=>,,6 8
x,,9 :
.,,: ;
id,,; =
),,= >
;,,> ?
}-- 
)-- 
;-- 
migrationBuilder// 
.// 
CreateTable// (
(//( )
name00 
:00 
$str00 
,00 
columns11 
:11 
table11 
=>11 !
new11" %
{22 
id33 
=33 
table33 
.33 
Column33 %
<33% &
int33& )
>33) *
(33* +
type33+ /
:33/ 0
$str331 :
,33: ;
nullable33< D
:33D E
false33F K
)33K L
.44 

Annotation44 #
(44# $
$str44$ D
,44D E)
NpgsqlValueGenerationStrategy44F c
.44c d#
IdentityByDefaultColumn44d {
)44{ |
,44| }

idCustomer55 
=55  
table55! &
.55& '
Column55' -
<55- .
int55. 1
>551 2
(552 3
type553 7
:557 8
$str559 B
,55B C
nullable55D L
:55L M
false55N S
)55S T
,55T U
idSeller66 
=66 
table66 $
.66$ %
Column66% +
<66+ ,
int66, /
>66/ 0
(660 1
type661 5
:665 6
$str667 @
,66@ A
nullable66B J
:66J K
false66L Q
)66Q R
,66R S

totalPrice77 
=77  
table77! &
.77& '
Column77' -
<77- .
decimal77. 5
>775 6
(776 7
type777 ;
:77; <
$str77= F
,77F G
nullable77H P
:77P Q
false77R W
)77W X
,77X Y
saleDate88 
=88 
table88 $
.88$ %
Column88% +
<88+ ,
DateTime88, 4
>884 5
(885 6
type886 :
:88: ;
$str88< V
,88V W
nullable88X `
:88` a
false88b g
)88g h
}99 
,99 
constraints:: 
::: 
table:: "
=>::# %
{;; 
table<< 
.<< 

PrimaryKey<< $
(<<$ %
$str<<% /
,<</ 0
x<<1 2
=><<3 5
x<<6 7
.<<7 8
id<<8 :
)<<: ;
;<<; <
}== 
)== 
;== 
migrationBuilder?? 
.?? 
CreateTable?? (
(??( )
name@@ 
:@@ 
$str@@ "
,@@" #
columnsAA 
:AA 
tableAA 
=>AA !
newAA" %
{BB 
idCC 
=CC 
tableCC 
.CC 
ColumnCC %
<CC% &
intCC& )
>CC) *
(CC* +
typeCC+ /
:CC/ 0
$strCC1 :
,CC: ;
nullableCC< D
:CCD E
falseCCF K
)CCK L
.DD 

AnnotationDD #
(DD# $
$strDD$ D
,DDD E)
NpgsqlValueGenerationStrategyDDF c
.DDc d#
IdentityByDefaultColumnDDd {
)DD{ |
,DD| }
idSaleEE 
=EE 
tableEE "
.EE" #
ColumnEE# )
<EE) *
intEE* -
>EE- .
(EE. /
typeEE/ 3
:EE3 4
$strEE5 >
,EE> ?
nullableEE@ H
:EEH I
falseEEJ O
)EEO P
,EEP Q
	idProductFF 
=FF 
tableFF  %
.FF% &
ColumnFF& ,
<FF, -
intFF- 0
>FF0 1
(FF1 2
typeFF2 6
:FF6 7
$strFF8 A
,FFA B
nullableFFC K
:FFK L
falseFFM R
)FFR S
,FFS T
quantityGG 
=GG 
tableGG $
.GG$ %
ColumnGG% +
<GG+ ,
intGG, /
>GG/ 0
(GG0 1
typeGG1 5
:GG5 6
$strGG7 @
,GG@ A
nullableGGB J
:GGJ K
falseGGL Q
)GGQ R
,GGR S
priceHH 
=HH 
tableHH !
.HH! "
ColumnHH" (
<HH( )
decimalHH) 0
>HH0 1
(HH1 2
typeHH2 6
:HH6 7
$strHH8 A
,HHA B
nullableHHC K
:HHK L
falseHHM R
)HHR S
,HHS T
SaleidII 
=II 
tableII "
.II" #
ColumnII# )
<II) *
intII* -
>II- .
(II. /
typeII/ 3
:II3 4
$strII5 >
,II> ?
nullableII@ H
:IIH I
trueIIJ N
)IIN O
}JJ 
,JJ 
constraintsKK 
:KK 
tableKK "
=>KK# %
{LL 
tableMM 
.MM 

PrimaryKeyMM $
(MM$ %
$strMM% 4
,MM4 5
xMM6 7
=>MM8 :
xMM; <
.MM< =
idMM= ?
)MM? @
;MM@ A
tableNN 
.NN 

ForeignKeyNN $
(NN$ %
nameOO 
:OO 
$strOO :
,OO: ;
columnPP 
:PP 
xPP  !
=>PP" $
xPP% &
.PP& '
SaleidPP' -
,PP- .
principalTableQQ &
:QQ& '
$strQQ( /
,QQ/ 0
principalColumnRR '
:RR' (
$strRR) -
)RR- .
;RR. /
}SS 
)SS 
;SS 
migrationBuilderUU 
.UU 
CreateIndexUU (
(UU( )
nameVV 
:VV 
$strVV ,
,VV, -
tableWW 
:WW 
$strWW #
,WW# $
columnXX 
:XX 
$strXX  
)XX  !
;XX! "
}YY 	
	protected\\ 
override\\ 
void\\ 
Down\\  $
(\\$ %
MigrationBuilder\\% 5
migrationBuilder\\6 F
)\\F G
{]] 	
migrationBuilder^^ 
.^^ 
	DropTable^^ &
(^^& '
name__ 
:__ 
$str__ !
)__! "
;__" #
migrationBuilderaa 
.aa 
	DropTableaa &
(aa& '
namebb 
:bb 
$strbb  
)bb  !
;bb! "
migrationBuilderdd 
.dd 
	DropTabledd &
(dd& '
nameee 
:ee 
$stree "
)ee" #
;ee# $
migrationBuildergg 
.gg 
	DropTablegg &
(gg& '
namehh 
:hh 
$strhh 
)hh 
;hh 
}ii 	
}jj 
}kk ˝;
WC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\DTOs\Seller\SellerDto.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Dtos  $
{ 
public 

class 
SellerCreateDto  
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[		 	
	MaxLength			 
(		 
$num		 
,		 
ErrorMessage		 #
=		$ %
$str		& Z
)		Z [
]		[ \
[

 	
	MinLength

	 
(

 
$num

 
,

 
ErrorMessage

 "
=

# $
$str

% W
)

W X
]

X Y
public 
required 
string 
Ci !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% W
)W X
]X Y
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Range	 
( 
$num 
, 
$num 
, 
ErrorMessage #
=$ %
$str& P
)P Q
]Q R
public 
required 
int 
Age 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% h
)h i
]i j
public 
string 
? 
Email 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Phone	 
( 
ErrorMessage 
= 
$str S
)S T
]T U
public 
string 
? 
Phone 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
public 
string 
? 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[   	
Range  	 
(   
$num   
,   
double   
.   
MaxValue   #
,  # $
ErrorMessage  % 1
=  2 3
$str  4 Y
)  Y Z
]  Z [
public!! 
decimal!! 
Salary!! 
{!! 
get!!  #
;!!# $
set!!% (
;!!( )
}!!* +
}## 
public%% 

class%% 
SellerUpdateDto%%  
{&& 
['' 	
Required''	 
('' 
ErrorMessage'' 
=''  
$str''! ?
)''? @
]''@ A
[(( 	
	MaxLength((	 
((( 
$num(( 
,(( 
ErrorMessage(( $
=((% &
$str((' [
)(([ \
]((\ ]
[)) 	
	MinLength))	 
()) 
$num)) 
,)) 
ErrorMessage)) "
=))# $
$str))% W
)))W X
]))X Y
public** 
required** 
string** 
Name** #
{**$ %
get**& )
;**) *
set**+ .
;**. /
}**0 1
[,, 	
Required,,	 
(,, 
ErrorMessage,, 
=,,  
$str,,! ?
),,? @
],,@ A
[-- 	
Range--	 
(-- 
$num-- 
,-- 
$num-- 
,-- 
ErrorMessage-- #
=--$ %
$str--& P
)--P Q
]--Q R
public.. 
required.. 
int.. 
Age.. 
{..  !
get.." %
;..% &
set..' *
;..* +
}.., -
[00 	
EmailAddress00	 
(00 
ErrorMessage00 "
=00# $
$str00% h
)00h i
]00i j
public11 
string11 
?11 
Email11 
{11 
get11 "
;11" #
set11$ '
;11' (
}11) *
[33 	
Phone33	 
(33 
ErrorMessage33 
=33 
$str33 S
)33S T
]33T U
public44 
string44 
?44 
Phone44 
{44 
get44 "
;44" #
set44$ '
;44' (
}44) *
[66 	
	MaxLength66	 
(66 
$num66 
,66 
ErrorMessage66 $
=66% &
$str66' [
)66[ \
]66\ ]
public77 
string77 
?77 
Address77 
{77  
get77! $
;77$ %
set77& )
;77) *
}77+ ,
[99 	
Required99	 
(99 
ErrorMessage99 
=99  
$str99! ?
)99? @
]99@ A
[:: 	
Range::	 
(:: 
$num:: 
,:: 
double:: 
.:: 
MaxValue:: #
,::# $
ErrorMessage::% 1
=::2 3
$str::4 Y
)::Y Z
]::Z [
public;; 
decimal;; 
Salary;; 
{;; 
get;;  #
;;;# $
set;;% (
;;;( )
};;* +
}<< 
public>> 

class>> 
SellerReadDto>> 
{?? 
public@@ 
int@@ 
Id@@ 
{@@ 
get@@ 
;@@ 
set@@  
;@@  !
}@@" #
publicAA 
requiredAA 
stringAA 
NameAA #
{AA$ %
getAA& )
;AA) *
setAA+ .
;AA. /
}AA0 1
publicBB 
requiredBB 
intBB 
AgeBB 
{BB  !
getBB" %
;BB% &
setBB' *
;BB* +
}BB, -
publicCC 
stringCC 
?CC 
EmailCC 
{CC 
getCC "
;CC" #
setCC$ '
;CC' (
}CC) *
publicDD 
stringDD 
?DD 
PhoneDD 
{DD 
getDD "
;DD" #
setDD$ '
;DD' (
}DD) *
publicEE 
stringEE 
?EE 
AddressEE 
{EE  
getEE! $
;EE$ %
setEE& )
;EE) *
}EE+ ,
publicFF 
decimalFF 
SalaryFF 
{FF 
getFF  #
;FF# $
setFF% (
;FF( )
}FF* +
}GG 
}HH Ó-
SC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\DTOs\Sale\SaleDto.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Dtos  $
{ 
public 

class 
SaleCreateDto 
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
public		 
required		 
int		 

IdCustomer		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
public 
required 
int 
IdSeller $
{% &
get' *
;* +
set, /
;/ 0
}1 2
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
,$ %
ErrorMessage& 2
=3 4
$str5 Z
)Z [
][ \
public 
decimal 

TotalPrice !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
DataType	 
( 
DataType 
. 
Date 
,  
ErrorMessage! -
=. /
$str0 Y
)Y Z
]Z [
public 
DateTime 
SaleDate  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% P
)P Q
]Q R
public 
List 
< 
SaleDetailCreateDto '
>' (
?( )
SaleDetails* 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
} 
public 

class 
SaleUpdateDto 
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
public 
required 
int 

IdCustomer &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
public   
required   
int   
IdSeller   $
{  % &
get  ' *
;  * +
set  , /
;  / 0
}  1 2
["" 	
Required""	 
("" 
ErrorMessage"" 
=""  
$str""! ?
)""? @
]""@ A
[## 	
Range##	 
(## 
$num## 
,## 
double## 
.## 
MaxValue## $
,##$ %
ErrorMessage##& 2
=##3 4
$str##5 Z
)##Z [
]##[ \
public$$ 
decimal$$ 

TotalPrice$$ !
{$$" #
get$$$ '
;$$' (
set$$) ,
;$$, -
}$$. /
[&& 	
Required&&	 
(&& 
ErrorMessage&& 
=&&  
$str&&! ?
)&&? @
]&&@ A
['' 	
DataType''	 
('' 
DataType'' 
.'' 
Date'' 
,''  
ErrorMessage''! -
=''. /
$str''0 Y
)''Y Z
]''Z [
public(( 
DateTime(( 
SaleDate((  
{((! "
get((# &
;((& '
set((( +
;((+ ,
}((- .
[** 	
Required**	 
(** 
ErrorMessage** 
=**  
$str**! ?
)**? @
]**@ A
[++ 	
	MinLength++	 
(++ 
$num++ 
,++ 
ErrorMessage++ "
=++# $
$str++% P
)++P Q
]++Q R
public,, 
List,, 
<,, 
SaleDetailUpdateDto,, '
>,,' (
?,,( )
SaleDetails,,* 5
{,,6 7
get,,8 ;
;,,; <
set,,= @
;,,@ A
},,B C
}-- 
public// 

class// 
SaleReadDto// 
{00 
public11 
int11 
Id11 
{11 
get11 
;11 
set11  
;11  !
}11" #
public22 
required22 
int22 

IdCustomer22 &
{22' (
get22) ,
;22, -
set22. 1
;221 2
}223 4
public33 
required33 
int33 
IdSeller33 $
{33% &
get33' *
;33* +
set33, /
;33/ 0
}331 2
public44 
decimal44 

TotalPrice44 !
{44" #
get44$ '
;44' (
set44) ,
;44, -
}44. /
public55 
DateTime55 
SaleDate55  
{55! "
get55# &
;55& '
set55( +
;55+ ,
}55- .
public66 
List66 
<66 
SaleDetailReadDto66 %
>66% &
?66& '
SaleDetails66( 3
{664 5
get666 9
;669 :
set66; >
;66> ?
}66@ A
}77 
}88 ⁄
YC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\DTOs\Sale\SaleDetailDto.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Dtos  $
{ 
public 

class 
SaleDetailCreateDto $
{ 
[		 	
Required			 
(		 
ErrorMessage		 
=		  
$str		! ?
)		? @
]		@ A
public

 
required

 
int

 
	IdProduct

 %
{

& '
get

( +
;

+ ,
set

- 0
;

0 1
}

2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ T
)T U
]U V
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
,$ %
ErrorMessage& 2
=3 4
$str5 Z
)Z [
][ \
public 
decimal 
	UnitPrice  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
public 

class 
SaleDetailUpdateDto $
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
public 
required 
int 
	IdProduct %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ T
)T U
]U V
public 
int 
Quantity 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! ?
)? @
]@ A
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
,$ %
ErrorMessage& 2
=3 4
$str5 Z
)Z [
][ \
public   
decimal   
	UnitPrice    
{  ! "
get  # &
;  & '
set  ( +
;  + ,
}  - .
}!! 
public## 

class## 
SaleDetailReadDto## "
{$$ 
public%% 
int%% 
Id%% 
{%% 
get%% 
;%% 
set%%  
;%%  !
}%%" #
public&& 
required&& 
int&& 
	IdProduct&& %
{&&& '
get&&( +
;&&+ ,
set&&- 0
;&&0 1
}&&2 3
public'' 
int'' 
Quantity'' 
{'' 
get'' !
;''! "
set''# &
;''& '
}''( )
public(( 
decimal(( 
	UnitPrice((  
{((! "
get((# &
;((& '
set((( +
;((+ ,
}((- .
})) 
}** ¡&
YC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\DTOs\Product\ProductDto.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Services  (
.( )
Dtos) -
{ 
public 

class 
ProductCreateDto !
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
[		 	
	MinLength			 
(		 
$num		 
,		 
ErrorMessage		 "
=		# $
$str		% W
)		W X
]		X Y
public

 
required

 
string

 
Name

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Range	 
( 
$num 
, 
double 
. 
MaxValue $
,$ %
ErrorMessage& 2
=3 4
$str5 Z
)Z [
][ \
public 
required 
decimal 
Price  %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
, 
ErrorMessage  ,
=- .
$str/ T
)T U
]U V
public 
int 
Stock 
{ 
get 
; 
set  #
;# $
}% &
} 
public 

class 
ProductUpdateDto !
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% W
)W X
]X Y
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
[   	
Required  	 
]   
[!! 	
Range!!	 
(!! 
$num!! 
,!! 
double!! 
.!! 
MaxValue!! $
,!!$ %
ErrorMessage!!& 2
=!!3 4
$str!!5 Z
)!!Z [
]!![ \
public"" 
required"" 
decimal"" 
Price""  %
{""& '
get""( +
;""+ ,
set""- 0
;""0 1
}""2 3
[$$ 	
Required$$	 
]$$ 
[%% 	
Range%%	 
(%% 
$num%% 
,%% 
int%% 
.%% 
MaxValue%% 
,%% 
ErrorMessage%%  ,
=%%- .
$str%%/ T
)%%T U
]%%U V
public&& 
required&& 
int&& 
Stock&& !
{&&" #
get&&$ '
;&&' (
set&&) ,
;&&, -
}&&. /
}'' 
public)) 

class)) 
ProductReadDto)) 
{** 
public++ 
int++ 
Id++ 
{++ 
get++ 
;++ 
set++  
;++  !
}++" #
public,, 
required,, 
string,, 
Name,, #
{,,$ %
get,,& )
;,,) *
set,,+ .
;,,. /
},,0 1
public-- 
string-- 
?-- 
Description-- "
{--# $
get--% (
;--( )
set--* -
;--- .
}--/ 0
public.. 
decimal.. 
Price.. 
{.. 
get.. "
;.." #
set..$ '
;..' (
}..) *
public// 
int// 
Stock// 
{// 
get// 
;// 
set//  #
;//# $
}//% &
}00 
}11 Â,
[C:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\DTOs\Customer\CustomerDto.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Dtos  $
{ 
public 

class 
CustomerCreateDto "
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage #
=$ %
$str& Z
)Z [
][ \
[		 	
	MinLength			 
(		 
$num		 
,		 
ErrorMessage		 "
=		# $
$str		% W
)		W X
]		X Y
public

 
required

 
string

 
Ci

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% W
)W X
]X Y
public 
required 
string 
Name #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% h
)h i
]i j
public 
string 
? 
Email 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
Phone	 
( 
ErrorMessage 
= 
$str S
)S T
]T U
public 
string 
? 
Phone 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage $
=% &
$str' [
)[ \
]\ ]
public 
string 
? 
Address 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
public 

class 
CustomerUpdateDto "
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
, 
ErrorMessage #
=$ %
$str& Z
)Z [
][ \
[ 	
	MinLength	 
( 
$num 
, 
ErrorMessage "
=# $
$str% W
)W X
]X Y
public   
required   
string   
Ci   !
{  " #
get  $ '
;  ' (
set  ) ,
;  , -
}  . /
["" 	
Required""	 
]"" 
[## 	
	MaxLength##	 
(## 
$num## 
,## 
ErrorMessage## $
=##% &
$str##' [
)##[ \
]##\ ]
[$$ 	
	MinLength$$	 
($$ 
$num$$ 
,$$ 
ErrorMessage$$ "
=$$# $
$str$$% W
)$$W X
]$$X Y
public%% 
required%% 
string%% 
Name%% #
{%%$ %
get%%& )
;%%) *
set%%+ .
;%%. /
}%%0 1
['' 	
EmailAddress''	 
('' 
ErrorMessage'' "
=''# $
$str''% h
)''h i
]''i j
public(( 
string(( 
?(( 
Email(( 
{(( 
get(( "
;((" #
set(($ '
;((' (
}(() *
[** 	
Phone**	 
(** 
ErrorMessage** 
=** 
$str** S
)**S T
]**T U
public++ 
string++ 
?++ 
Phone++ 
{++ 
get++ "
;++" #
set++$ '
;++' (
}++) *
[-- 	
	MaxLength--	 
(-- 
$num-- 
,-- 
ErrorMessage-- $
=--% &
$str--' [
)--[ \
]--\ ]
public.. 
string.. 
?.. 
Address.. 
{..  
get..! $
;..$ %
set..& )
;..) *
}..+ ,
}// 
public11 

class11 
CustomerReadDto11  
{22 
public33 
int33 
Id33 
{33 
get33 
;33 
set33  
;33  !
}33" #
public44 
required44 
string44 
Ci44 !
{44" #
get44$ '
;44' (
set44) ,
;44, -
}44. /
public55 
required55 
string55 
Name55 #
{55$ %
get55& )
;55) *
set55+ .
;55. /
}550 1
public66 
string66 
?66 
Email66 
{66 
get66 "
;66" #
set66$ '
;66' (
}66) *
public77 
string77 
?77 
Phone77 
{77 
get77 "
;77" #
set77$ '
;77' (
}77) *
public88 
string88 
?88 
Address88 
{88  
get88! $
;88$ %
set88& )
;88) *
}88+ ,
}99 
}:: ﬁ+
eC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Controllers\Seller\SellerController.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Controllers  +
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public		 

class		 
SellerController		 !
:		" #
ControllerBase		$ 2
{

 
private 
readonly 
ISellerService '
_sellerService( 6
;6 7
public 
SellerController 
(  
ISellerService  .
sellerService/ <
)< =
{ 	
_sellerService 
= 
sellerService *
;* +
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAll) /
(/ 0
)0 1
{ 	
var 
sellers 
= 
await 
_sellerService  .
.. /
GetAllAsync/ :
(: ;
); <
;< =
return 
Ok 
( 
sellers 
) 
; 
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetById) 0
(0 1
int1 4
id5 7
)7 8
{ 	
var 
seller 
= 
await 
_sellerService -
.- .
GetByIdAsync. :
(: ;
id; =
)= >
;> ?
if 
( 
seller 
== 
null 
) 
return 
NotFound 
(  
)  !
;! "
return   
Ok   
(   
seller   
)   
;   
}!! 	
[## 	
HttpPost##	 
]## 
public$$ 
async$$ 
Task$$ 
<$$ 
IActionResult$$ '
>$$' (
Create$$) /
($$/ 0
[$$0 1
FromBody$$1 9
]$$9 :
SellerCreateDto$$; J
seller$$K Q
)$$Q R
{%% 	
if&& 
(&& 
!&& 

ModelState&& 
.&& 
IsValid&& #
)&&# $
return'' 

BadRequest'' !
(''! "

ModelState''" ,
)'', -
;''- .
var)) 
createdSeller)) 
=)) 
await))  %
_sellerService))& 4
.))4 5
CreateAsync))5 @
())@ A
seller))A G
)))G H
;))H I
return** 
CreatedAtAction** "
(**" #
nameof**# )
(**) *
GetById*** 1
)**1 2
,**2 3
new**4 7
{**8 9
id**: <
=**= >
createdSeller**? L
.**L M
Id**M O
}**P Q
,**Q R
createdSeller**S `
)**` a
;**a b
}++ 	
[-- 	
HttpPut--	 
(-- 
$str-- 
)-- 
]-- 
public.. 
async.. 
Task.. 
<.. 
IActionResult.. '
>..' (
Update..) /
(../ 0
int..0 3
id..4 6
,..6 7
[..8 9
FromBody..9 A
]..A B
SellerUpdateDto..C R
seller..S Y
)..Y Z
{// 	
if00 
(00 
!00 

ModelState00 
.00 
IsValid00 #
)00# $
return11 

BadRequest11 !
(11! "

ModelState11" ,
)11, -
;11- .
var33 
updated33 
=33 
await33 
_sellerService33  .
.33. /
UpdateAsync33/ :
(33: ;
id33; =
,33= >
seller33? E
)33E F
;33F G
if44 
(44 
updated44 
==44 
null44 
)44  
return55 
NotFound55 
(55  
$"55  "
$str55" 2
{552 3
id553 5
}555 6
$str556 E
"55E F
)55F G
;55G H
return77 
Ok77 
(77 
updated77 
)77 
;77 
}88 	
[:: 	

HttpDelete::	 
(:: 
$str:: 
):: 
]:: 
public;; 
async;; 
Task;; 
<;; 
IActionResult;; '
>;;' (
Delete;;) /
(;;/ 0
int;;0 3
id;;4 6
);;6 7
{<< 	
var== 
deleted== 
=== 
await== 
_sellerService==  .
.==. /
DeleteAsync==/ :
(==: ;
id==; =
)=== >
;==> ?
if>> 
(>> 
!>> 
deleted>> 
)>> 
return?? 
NotFound?? 
(??  
$"??  "
$str??" 2
{??2 3
id??3 5
}??5 6
$str??6 E
"??E F
)??F G
;??G H
returnAA 
	NoContentAA 
(AA 
)AA 
;AA 
}BB 	
}CC 
}DD ø*
aC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Controllers\Sale\SaleController.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Controllers  +
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public		 

class		 
SaleController		 
:		  !
ControllerBase		" 0
{

 
private 
readonly 
ISaleService %
_saleService& 2
;2 3
public 
SaleController 
( 
ISaleService *
saleService+ 6
)6 7
{ 	
_saleService 
= 
saleService &
;& '
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAll) /
(/ 0
)0 1
{ 	
var 
sales 
= 
await 
_saleService *
.* +
GetAllAsync+ 6
(6 7
)7 8
;8 9
return 
Ok 
( 
sales 
) 
; 
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetById) 0
(0 1
int1 4
id5 7
)7 8
{ 	
var 
sale 
= 
await 
_saleService )
.) *
GetByIdAsync* 6
(6 7
id7 9
)9 :
;: ;
if 
( 
sale 
== 
null 
) 
return 
NotFound 
(  
)  !
;! "
return 
Ok 
( 
sale 
) 
; 
}   	
["" 	
HttpPost""	 
]"" 
public## 
async## 
Task## 
<## 
IActionResult## '
>##' (
Create##) /
(##/ 0
[##0 1
FromBody##1 9
]##9 :
SaleCreateDto##; H
sale##I M
)##M N
{$$ 	
if%% 
(%% 
!%% 

ModelState%% 
.%% 
IsValid%% #
)%%# $
return&& 

BadRequest&& !
(&&! "

ModelState&&" ,
)&&, -
;&&- .
var(( 
createdSale(( 
=(( 
await(( #
_saleService(($ 0
.((0 1
CreateAsync((1 <
(((< =
sale((= A
)((A B
;((B C
return)) 
CreatedAtAction)) "
())" #
nameof))# )
())) *
GetById))* 1
)))1 2
,))2 3
new))4 7
{))8 9
id)): <
=))= >
createdSale))? J
.))J K
Id))K M
}))N O
,))O P
createdSale))Q \
)))\ ]
;))] ^
}** 	
[,, 	
HttpPut,,	 
(,, 
$str,, 
),, 
],, 
public-- 
async-- 
Task-- 
<-- 
IActionResult-- '
>--' (
Update--) /
(--/ 0
int--0 3
id--4 6
,--6 7
[--8 9
FromBody--9 A
]--A B
SaleUpdateDto--C P
sale--Q U
)--U V
{.. 	
if// 
(// 
!// 

ModelState// 
.// 
IsValid// #
)//# $
return00 

BadRequest00 !
(00! "

ModelState00" ,
)00, -
;00- .
var22 
updatedSale22 
=22 
await22 #
_saleService22$ 0
.220 1
UpdateAsync221 <
(22< =
id22= ?
,22? @
sale22A E
)22E F
;22F G
if33 
(33 
updatedSale33 
==33 
null33 #
)33# $
return44 
NotFound44 
(44  
$"44  "
$str44" /
{44/ 0
id440 2
}442 3
$str443 B
"44B C
)44C D
;44D E
return66 
Ok66 
(66 
updatedSale66 !
)66! "
;66" #
}77 	
[99 	

HttpDelete99	 
(99 
$str99 
)99 
]99 
public:: 
async:: 
Task:: 
<:: 
IActionResult:: '
>::' (
Delete::) /
(::/ 0
int::0 3
id::4 6
)::6 7
{;; 	
var<< 
deleted<< 
=<< 
await<< 
_saleService<<  ,
.<<, -
DeleteAsync<<- 8
(<<8 9
id<<9 ;
)<<; <
;<<< =
if== 
(== 
!== 
deleted== 
)== 
return>> 
NotFound>> 
(>>  
)>>  !
;>>! "
return@@ 
	NoContent@@ 
(@@ 
)@@ 
;@@ 
}AA 	
}BB 
}CC »(
gC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Controllers\Product\ProductController.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Controllers  +
{ 
[ 
ApiController 
] 
[		 
Route		 

(		
 
$str		 
)		 
]		 
public

 

class

 
ProductController

 "
:

# $
ControllerBase

% 3
{ 
private 
readonly 
IProductService (
_productService) 8
;8 9
public 
ProductController  
(  !
IProductService! 0
productService1 ?
)? @
{ 	
_productService 
= 
productService ,
;, -
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAll) /
(/ 0
)0 1
{ 	
var 
products 
= 
await  
_productService! 0
.0 1
GetAllAsync1 <
(< =
)= >
;> ?
return 
Ok 
( 
products 
) 
;  
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetById) 0
(0 1
int1 4
id5 7
)7 8
{ 	
var 
product 
= 
await 
_productService  /
./ 0
GetByIdAsync0 <
(< =
id= ?
)? @
;@ A
if 
( 
product 
== 
null 
)  
return 
NotFound 
(  
)  !
;! "
return   
Ok   
(   
product   
)   
;   
}!! 	
[## 	
HttpPost##	 
]## 
public$$ 
async$$ 
Task$$ 
<$$ 
IActionResult$$ '
>$$' (
Create$$) /
($$/ 0
[$$0 1
FromBody$$1 9
]$$9 :
ProductCreateDto$$; K
product$$L S
)$$S T
{%% 	
if&& 
(&& 
!&& 

ModelState&& 
.&& 
IsValid&& #
)&&# $
{'' 
return(( 

BadRequest(( !
(((! "

ModelState((" ,
)((, -
;((- .
})) 
var** 
createdProduct** 
=**  
await**! &
_productService**' 6
.**6 7
CreateAsync**7 B
(**B C
product**C J
)**J K
;**K L
return++ 
CreatedAtAction++ "
(++" #
nameof++# )
(++) *
GetById++* 1
)++1 2
,++2 3
new++4 7
{++8 9
id++: <
=++= >
createdProduct++? M
.++M N
Id++N P
}++Q R
,++R S
createdProduct++T b
)++b c
;++c d
},, 	
[.. 	
HttpPut..	 
(.. 
$str.. 
).. 
].. 
public// 
async// 
Task// 
<// 
IActionResult// '
>//' (
Update//) /
(/// 0
int//0 3
id//4 6
,//6 7
[//8 9
FromBody//9 A
]//A B
ProductUpdateDto//C S
product//T [
)//[ \
{00 	
var11 
result11 
=11 
await11 
_productService11 .
.11. /
UpdateAsync11/ :
(11: ;
id11; =
,11= >
product11? F
)11F G
;11G H
if22 
(22 
result22 
==22 
null22 
)22 
return33 
NotFound33 
(33  
)33  !
;33! "
return44 
Ok44 
(44 
result44 
)44 
;44 
}55 	
[77 	

HttpDelete77	 
(77 
$str77 
)77 
]77 
public88 
async88 
Task88 
<88 
IActionResult88 '
>88' (
Delete88) /
(88/ 0
int880 3
id884 6
)886 7
{99 	
var:: 
result:: 
=:: 
await:: 
_productService:: .
.::. /
DeleteAsync::/ :
(::: ;
id::; =
)::= >
;::> ?
if;; 
(;; 
!;; 
result;; 
);; 
return<< 
NotFound<< 
(<<  
)<<  !
;<<! "
return== 
	NoContent== 
(== 
)== 
;== 
}>> 	
}@@ 
}AA Î)
\C:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Configuration\AppDbContext.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Configuration  -
{ 
public 

class 
AppDbContext 
: 
	DbContext  )
{ 
public 
AppDbContext 
( 
DbContextOptions ,
<, -
AppDbContext- 9
>9 :
options; B
)B C
:D E
baseF J
(J K
optionsK R
)R S
{		 	
}

 	
public 
DbSet 
< 
Customer 
> 
	Customers (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 
DbSet 
< 
Product 
> 
Products &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
DbSet 
< 
Sale 
> 
Sales  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet 
< 

SaleDetail 
>  
SaleDetails! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
DbSet 
< 
Seller 
> 
Sellers $
{% &
get' *
;* +
set, /
;/ 0
}1 2
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
base 
. 
OnModelCreating  
(  !
modelBuilder! -
)- .
;. /
modelBuilder 
. 
Entity 
<  
Sale  $
>$ %
(% &
)& '
. 
HasOne 
( 
s 
=> 
s 
. 
Customer '
)' (
. 
WithMany 
( 
c 
=> 
c  
.  !
Sales! &
)& '
. 
HasForeignKey 
( 
s  
=>! #
s$ %
.% &

IdCustomer& 0
)0 1
. 
OnDelete 
( 
DeleteBehavior (
.( )
Restrict) 1
)1 2
;2 3
modelBuilder 
. 
Entity 
<  
Sale  $
>$ %
(% &
)& '
. 
HasOne 
( 
s 
=> 
s 
. 
Seller %
)% &
.   
WithMany   
(   
se   
=>   
se    "
.  " #
Sales  # (
)  ( )
.!! 
HasForeignKey!! 
(!! 
s!!  
=>!!! #
s!!$ %
.!!% &
IdSeller!!& .
)!!. /
."" 
OnDelete"" 
("" 
DeleteBehavior"" (
.""( )
Restrict"") 1
)""1 2
;""2 3
modelBuilder%% 
.%% 
Entity%% 
<%%  

SaleDetail%%  *
>%%* +
(%%+ ,
)%%, -
.&& 
HasOne&& 
(&& 
sd&& 
=>&& 
sd&&  
.&&  !
Sale&&! %
)&&% &
.'' 
WithMany'' 
('' 
s'' 
=>'' 
s''  
.''  !
SaleDetails''! ,
)'', -
.(( 
HasForeignKey(( 
((( 
sd(( !
=>((" $
sd((% '
.((' (
IdSale((( .
)((. /
.)) 
OnDelete)) 
()) 
DeleteBehavior)) (
.))( )
Cascade))) 0
)))0 1
;))1 2
modelBuilder,, 
.,, 
Entity,, 
<,,  

SaleDetail,,  *
>,,* +
(,,+ ,
),,, -
.-- 
HasOne-- 
(-- 
sd-- 
=>-- 
sd--  
.--  !
Product--! (
)--( )
... 
WithMany.. 
(.. 
p.. 
=>.. 
p..  
...  !
SaleDetails..! ,
).., -
.// 
HasForeignKey// 
(// 
sd// !
=>//" $
sd//% '
.//' (
	IdProduct//( 1
)//1 2
.00 
OnDelete00 
(00 
DeleteBehavior00 (
.00( )
Restrict00) 1
)001 2
;002 3
modelBuilder33 
.33 
Entity33 
<33  
Customer33  (
>33( )
(33) *
)33* +
.44 
HasIndex44 
(44 
c44 
=>44 
c44  
.44  !
Ci44! #
)44# $
.55 
IsUnique55 
(55 
)55 
;55 
}77 	
}88 
}99 ﬂ'
iC:\Diplomado\Proyecto\LubricantesAyrthon\LubricantesAyrthonAPI\Controllers\Customer\CustomerController.cs
	namespace 	!
LubricantesAyrthonAPI
 
.  
Controllers  +
{		 
[

 
ApiController

 
]

 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
CustomerController #
:$ %
ControllerBase& 4
{ 
private 
readonly 
ICustomerService )
_customerService* :
;: ;
public 
CustomerController !
(! "
ICustomerService" 2
customerService3 B
)B C
{ 	
_customerService 
= 
customerService .
;. /
} 	
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetAll) /
(/ 0
)0 1
{ 	
var 
	customers 
= 
await !
_customerService" 2
.2 3
GetAllAsync3 >
(> ?
)? @
;@ A
return 
Ok 
( 
	customers 
)  
;  !
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
IActionResult '
>' (
GetById) 0
(0 1
int1 4
id5 7
)7 8
{ 	
var 
customer 
= 
await  
_customerService! 1
.1 2
GetByIdAsync2 >
(> ?
id? A
)A B
;B C
if 
( 
customer 
== 
null  
)  !
return   
NotFound   
(    
)    !
;  ! "
return!! 
Ok!! 
(!! 
customer!! 
)!! 
;!!  
}"" 	
[$$ 	
HttpPost$$	 
]$$ 
public%% 
async%% 
Task%% 
<%% 
IActionResult%% '
>%%' (
Create%%) /
(%%/ 0
CustomerCreateDto%%0 A
customer%%B J
)%%J K
{&& 	
if(( 
((( 
!(( 

ModelState(( 
.(( 
IsValid(( #
)((# $
return)) 

BadRequest)) !
())! "

ModelState))" ,
))), -
;))- .
var** 
createdCustomer** 
=**  !
await**" '
_customerService**( 8
.**8 9
CreateAsync**9 D
(**D E
customer**E M
)**M N
;**N O
return++ 
CreatedAtAction++ "
(++" #
nameof++# )
(++) *
GetById++* 1
)++1 2
,++2 3
new++4 7
{++8 9
id++: <
=++= >
createdCustomer++? N
.++N O
Id++O Q
}++R S
,++S T
createdCustomer++U d
)++d e
;++e f
},, 	
[.. 	
HttpPut..	 
(.. 
$str.. 
).. 
].. 
public// 
async// 
Task// 
<// 
IActionResult// '
>//' (
Update//) /
(/// 0
int//0 3
id//4 6
,//6 7
CustomerUpdateDto//8 I
customer//J R
)//R S
{00 	
var11 
result11 
=11 
await11 
_customerService11 /
.11/ 0
UpdateAsync110 ;
(11; <
id11< >
,11> ?
customer11@ H
)11H I
;11I J
if22 
(22 
result22 
==22 
null22 
)22 
return33 
NotFound33 
(33  
)33  !
;33! "
return44 
Ok44 
(44 
result44 
)44 
;44 
}55 	
[77 	

HttpDelete77	 
(77 
$str77 
)77 
]77 
public88 
async88 
Task88 
<88 
IActionResult88 '
>88' (
Delete88) /
(88/ 0
int880 3
id884 6
)886 7
{99 	
var:: 
result:: 
=:: 
await:: 
_customerService:: /
.::/ 0
DeleteAsync::0 ;
(::; <
id::< >
)::> ?
;::? @
if;; 
(;; 
!;; 
result;; 
);; 
return<< 
NotFound<< 
(<<  
)<<  !
;<<! "
return== 
	NoContent== 
(== 
)== 
;== 
}>> 	
}?? 
}@@ 