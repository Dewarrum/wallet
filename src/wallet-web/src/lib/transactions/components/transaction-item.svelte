<script lang="ts">
	import { TransactionType, type Transaction } from '$lib/transactions/models';
	import { Badge } from '../../components/ui/badge';
	import * as Table from '../../components/ui/table';

	export let transaction: Transaction;
</script>

<Table.Row>
	<Table.Cell>
		<div class="font-medium">{transaction.type}</div>
		<div class="hidden text-sm text-muted-foreground md:inline">
			{transaction.description}
		</div>
	</Table.Cell>
	<Table.Cell class="xl:table.-column hidden">{transaction.type}</Table.Cell>
	<Table.Cell class="xl:table.-column hidden">
		<Badge class="text-xs" variant="outline">Status</Badge>
	</Table.Cell>
	<Table.Cell class="md:table.-cell xl:table.-column hidden lg:hidden"
		>{transaction.createdAt}</Table.Cell
	>
	<Table.Cell class="text-right"
		><span
			class:text-red-700={transaction.type === TransactionType.Expense}
			class:text-green-700={transaction.type === TransactionType.Income}>${transaction.amount}</span
		></Table.Cell
	>
</Table.Row>
