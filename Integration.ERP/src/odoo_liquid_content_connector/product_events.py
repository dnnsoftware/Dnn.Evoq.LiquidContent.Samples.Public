from odoo.addons.connector.event import (
    on_record_create,
    on_record_write
)

""" Enqueue jobs for exporting product to Liquid Content under some events: create/update products"""

@on_record_create(model_names='product.product')
def delay_export_product_product_create(env, model_name, record_id, values):
    _enqueue_export_jobs(env, model_name, record_id)


@on_record_write(model_names='product.product')
def delay_export_product_product_update(env, model_name, record_id, values):
    _enqueue_export_jobs(env, model_name, record_id)


def _enqueue_export_jobs(env, model_name, record_id):
    product = env[model_name].browse(record_id)
    backends = env['connector_sc.configuration.backend'].search([])
    for backend in backends:
        product.with_delay().export_product(record_id, backend.id)

@on_record_write(model_names='product.template')
def delay_export_product_template_update(env, model_name, record_id, values):
    product_model_name = 'product.product'
    products = env[product_model_name].search([('product_tmpl_id', '=', record_id)])

    for product in products:
        _enqueue_export_jobs(env, product_model_name, product.id)
