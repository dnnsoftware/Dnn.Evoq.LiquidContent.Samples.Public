from .product_adapter import LiquidContentProductAdapter
import datetime


class LiquidContentProductSynchronizer():
    """ Export products to Liquid Content """
    _product_adapter = LiquidContentProductAdapter()
    _model_name = 'product.product'
    _backend_model = 'connector_sc.configuration.backend'
    _linked_model_name = 'sc.product.product'

    def _export_product(self, env, product, backend):
        link_register = env[self._linked_model_name].search([('odoo_id', '=', product.id), ('backend_id', '=', backend.id)])

        if len(link_register) == 0:
            sc_id = self._product_adapter.create(product, backend)

            env[self._linked_model_name].create({
                'odoo_id': product.id,
                'sc_id': sc_id,
                'backend_id': backend.id,
                'sync_date': datetime.datetime.now()})
        else:
            self._product_adapter.update(link_register[0].sc_id, product, backend)

    def run(self, env, record_id, backend_id):
        """ Run the job to export the product """
        product = env[self._model_name].browse(record_id)
        backend = env[self._backend_model].browse(backend_id)
        self._export_product(env, product, backend)

