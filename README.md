Простая система инвентаря
===
Требования:
- Реализовывать все в 3D.
- Предметы должны использовать физику и гравитацию ( rigidbody ).
- 3 условных объекта, каждый имеет свой конфигуратор ( со следующими полями: вес, название, идентификатор, тип ).
- Создать объект "рюкзак" в который будут помещаться предметы.
- Подбирать предметы можно мышкой и закидывая их в "рюкзак" ( используя Drag & Drop ) они должны быть помещены "внутрь" и сохраниться.
- Каждый объект имеет разный тип.
- При наведении на "рюкзак", нажимая и не отпуская ЛКМ, отображается содержимое рюкзака ( простенькое UI ).
- При наведении на один из предметов ( все так же, не отпуская ЛКМ и отпустив на нем ЛКМ, предмет "достается" из "рюкзака".

- Каждый ивент складывания/доставания предмета из/в "рюкзак" отправляется запрос на сервер, с идентификатором предмета и его событием.
- Каждый ивент складывания/доставания сопровождается UnityEvent.
- Каждый тип объекта имеет свою уникальную позицию на UI рюкзака.
- Каждый тип объекта имеет свою уникальную позицию на модельке рюкзака ( должен прикрепляться на рюкзак ).
- Каждый объект плавно приснапливается к своему месту на рюкзаке и так же плавно из него вынимается.
- Версия Unity 2019.4 LTS.