from . import sc_content_items_service


class LiquidContentProductAdapter():
    """ Backend Adapter for the Liquid Content Product """

    def create(self, product, backend):

        content_item = self._get_payload(product, backend)
        return sc_content_items_service.add_content_item(content_item, backend)

    def _get_payload(self, product, backend):

        product_name = str(product.default_code) + ' ' + product.name
        return {
            'contentTypeId': backend.productTypeId,
            'description': product.description_sale,
            'details': {
                'description': product.description_sale,
                'imageUrl': 'http://localhost:8069/web/image?model=product.template&field=image_medium&id=' + str(product.product_tmpl_id.id),
                'name': product.name,
                'quantity': product.qty_available,
                'salePrice': product.lst_price,
                'code': product.default_code,
                'category': product.categ_id.name
            },
            'name': product_name,
            'tags': [product.categ_id.name]
        }

    def update(self, sc_id, product, backend):
        content_item = self._get_payload(product, backend)
        content_item['id'] = sc_id

        return sc_content_items_service.update_content_item(sc_id, content_item, backend)



