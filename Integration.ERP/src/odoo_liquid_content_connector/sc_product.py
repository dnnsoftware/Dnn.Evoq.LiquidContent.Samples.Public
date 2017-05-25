from odoo import models, fields
from odoo.addons.queue_job.job import job
from .product_synchronizer import LiquidContentProductSynchronizer


class LiquidContentProduct(models.Model):
    """ Extension to product model in Odoo to store information regarding the synchronized Liquid Content backend"""

    _name = 'sc.product.product'
    _inherits = {'product.product': 'odoo_id'}
    _description = 'SC Products'

    backend_id = fields.Many2one(comodel_name='connector_sc.configuration.backend', string='Liquid Content Backend', required=True, ondelete='restrict')
    odoo_id = fields.Many2one(comodel_name='product.product', string='Product', required=True, ondelete='cascade')
    sc_id = fields.Char(string='ID on Liquid Content')
    sync_date = fields.Datetime(string='Last synchronization date')


class LiquidContentProductExtendedMethods(models.Model):
    _inherit = 'product.product'

    @job
    def export_product(self, record_id, backend_id):
        product_exporter = LiquidContentProductSynchronizer()
        return product_exporter.run(self.env, record_id, backend_id)

